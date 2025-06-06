using bookingfootball.Common.Models;
using bookingfootball.Common.Reponse;
using bookingfootball.Constract;
using bookingfootball.Db_QL;
using bookingfootball.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using RegisterRequest = bookingfootball.Common.Request.RegisterRequest;

namespace bookingfootball.Service
{
    public class AuthService: IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<TaiKhoan> _passwordHasher;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IUserRepository userRepository, IConfiguration configuration, IPasswordHasher<TaiKhoan> passwordHasher, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Token> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null || !user.IsActive)
            {
                throw new UnauthorizedAccessException("Invalid email or user is inactive.");
            }

            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.MatKhau, password);
            if (passwordVerificationResult != PasswordVerificationResult.Success)
            {
                throw new UnauthorizedAccessException("Invalid password.");
            }

            var token = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return new Token
            {
                AccessToken = token,
                RefreshToken = refreshToken
            };
        }

        public async Task<Token> RegisterAsync(RegisterRequest request)
        {
            try
            {
                Console.WriteLine("===== REGISTER START =====");
                Console.WriteLine("Request: " + JsonConvert.SerializeObject(request));

                if (await _userRepository.EmailExistsAsync(request.Email))
                {
                    Console.WriteLine("Email đã tồn tại: " + request.Email);
                    throw new InvalidOperationException("Email is already registered.");
                }

                if (await _userRepository.UserNameExistsAsync(request.TenDangNhap))
                {
                    Console.WriteLine("Username đã tồn tại: " + request.TenDangNhap);
                    throw new InvalidOperationException("Username is already taken.");
                }

                var user = new TaiKhoan
                {
                    TenDangNhap = request.TenDangNhap,
                    Email = request.Email,
                    MatKhau = _passwordHasher.HashPassword(null, request.Password),
                    SoDienThoai = request.SoDienThoai,
                    Role = request.Role ?? Role.Customer,
                    IsActive = true,
                    NgayTao = DateTime.UtcNow
                };

                if (user.Role == Role.Customer)
                {
                    user.KhachHang = new KhachHang
                    {
                        HoTen = request.HoTen,
                        Email = request.Email,
                        SoDienThoai = request.SoDienThoai,
                        GioiTinh = request.GioiTinh,
                        IsActive = true,
                        CreateStamp = DateTime.UtcNow
                    };
                    Console.WriteLine("Tạo khách hàng");
                }
                else if (user.Role == Role.Staff)
                {
                    user.NhanVien = new NhanVien
                    {
                        FullName = request.HoTen,
                        Username = request.TenDangNhap,
                        Email = request.Email,
                        PhoneNumber = request.SoDienThoai,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow,
                        StaffCode = GenerateStaffCode(),
                        Address = request.Address ?? string.Empty,
                        IdentityNumber = request.IdentityNumber ?? string.Empty
                    };
                    Console.WriteLine("Tạo nhân viên");
                }

                await _userRepository.AddAsync(user);
                await _userRepository.SaveChangesAsync();
                Console.WriteLine("Đã lưu tài khoản vào DB");

                var token = GenerateJwtToken(user);
                var refreshToken = GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();
                Console.WriteLine("Đã cập nhật RefreshToken");

                Console.WriteLine("===== REGISTER SUCCESS =====");

                return new Token
                {
                    AccessToken = token,
                    RefreshToken = refreshToken
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("===== REGISTER ERROR =====");
                Console.WriteLine("Lỗi: " + ex.Message);
                Console.WriteLine("StackTrace: " + ex.StackTrace);
                throw;
            }
        }


        public async Task<Token> RefreshTokenAsync(string refreshToken)
        {
            var user = await _userRepository.FindAsync(u => u.RefreshToken == refreshToken && u.IsActive);
            var userResult = user.FirstOrDefault();
            if (userResult == null)
            {
                throw new SecurityTokenException("Invalid refresh token.");
            }

            var newAccessToken = GenerateJwtToken(userResult);
            var newRefreshToken = GenerateRefreshToken();

            userResult.RefreshToken = newRefreshToken;
            _userRepository.Update(userResult);
            await _userRepository.SaveChangesAsync();

            return new Token
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

        private string GenerateJwtToken(TaiKhoan user)
        {
            var claims = new ClaimsBuilder(user)
                .AddUserId()
                .AddUsername()
                .AddEmail()
                .AddRole()
                .Build();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["JwtSettings:TokenExpiryMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

        private string GenerateStaffCode()
        {
            return $"STAFF-{Guid.NewGuid().ToString().Substring(0, 8)}";
        }
        public async Task<CurrentUserResponse> GetCurrentUserAsync()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("Không tìm thấy thông tin người dùng từ token.");
            }

            var user = await _userRepository.GetByIdAsync(userId, asNoTracking: true);
            if (user == null || !user.IsActive)
            {
                throw new UnauthorizedAccessException("Tài khoản không tồn tại hoặc không hoạt động.");
            }

            string hoTen = user.Role == Role.Customer && user.KhachHang != null
                ? user.KhachHang.HoTen
                : user.Role == Role.Staff && user.NhanVien != null
                    ? user.NhanVien.FullName
                    : string.Empty;

            return new CurrentUserResponse
            {
                Id = user.Id,
                TenDangNhap = user.TenDangNhap,
                Email = user.Email,
                SoDienThoai = user.SoDienThoai,
                HoTen = hoTen,
                Role = user.Role
            };
        }

    }
}


