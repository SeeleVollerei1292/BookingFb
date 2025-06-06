using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using bookingfootball.Db_QL;

namespace bookingfootball.Common.Models
{
    public class ClaimsBuilder
    {
        private readonly List<Claim> _claims = new List<Claim>();
        private readonly TaiKhoan _user;

        public ClaimsBuilder(TaiKhoan user)
        {
            _user = user;
        }

        public ClaimsBuilder AddUsername()
        {
            string username = _user.Role == Role.Customer && _user.KhachHang != null
                ? $"{_user.KhachHang.HoTen} {_user.TenDangNhap}"
                : _user.Role == Role.Staff && _user.NhanVien != null
                    ? $"{_user.NhanVien.FullName} {_user.TenDangNhap}"
                    : _user.TenDangNhap; // Fallback for Admin or other roles
            _claims.Add(new Claim(ClaimTypes.Name, username));
            return this;
        }

        public ClaimsBuilder AddEmail()
        {
            _claims.Add(new Claim(ClaimTypes.Email, _user.Email));
            return this;
        }

        public ClaimsBuilder AddUserId()
        {
            _claims.Add(new Claim(ClaimTypes.NameIdentifier, _user.Id.ToString()));
            _claims.Add(new Claim("UserId", _user.Id.ToString()));
            return this;
        }

        public ClaimsBuilder AddFullname()
        {
            string fullName = _user.Role == Role.Customer && _user.KhachHang != null
                ? _user.KhachHang.HoTen
                : _user.Role == Role.Staff && _user.NhanVien != null
                    ? _user.NhanVien.FullName
                    : string.Empty; // Fallback for Admin or other roles
            _claims.Add(new Claim("Fullname", fullName));
            return this;
        }

        public ClaimsBuilder AddRole()
        {
            _claims.Add(new Claim(ClaimTypes.Role, _user.Role.ToString()));
            return this;
        }

        public List<Claim> Build() => _claims;
    }
}
