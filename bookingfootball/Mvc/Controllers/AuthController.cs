using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Mvc.Models;
using Mvc.Request;


namespace Mvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View(new LoginRequest());
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View(new LoginRequest());
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request); // Giữ lại dữ liệu và hiện lỗi
            }
            var apiUrl = "https://localhost:7054/auth/login";

            var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, jsonContent);


            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Đăng nhập thất bại";
                return View("SignIn");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<Token>(responseContent);

            // Gọi API /current-user
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            var userResponse = await _httpClient.GetAsync("https://localhost:7054/auth/current-user");
            if (!userResponse.IsSuccessStatusCode)
            {
                ViewBag.Error = "Không lấy được thông tin người dùng";
                return View("Index");
            }

            var userContent = await userResponse.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<CurrentUserResponse>(userContent);

            // Lưu cookie (claims) vào hệ thống xác thực của MVC
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.HoTen),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = true
                });

            // Có thể lưu token vào session nếu muốn
            HttpContext.Session.SetString("AccessToken", token.AccessToken);

            return RedirectToAction("Index", "Nhanvien", new { area = "Admin" });
        }

        [HttpGet]
        public IActionResult Info()
        {
            return View(); // View hiển thị thông tin user từ Claims
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("SignIn");
        }



        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterRequest());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request); // Giữ lại dữ liệu và hiện lỗi
            }
            try
            {
                var apiUrl = "https://localhost:7054/auth/register";
                var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(apiUrl, jsonContent);

                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("===== RESPONSE REGISTER =====");
                Console.WriteLine(responseContent);

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = $"Đăng ký thất bại: {responseContent}";
                    return View(request);
                }

                var token = JsonConvert.DeserializeObject<Token>(responseContent);
                HttpContext.Session.SetString("AccessToken", token.AccessToken);
                ViewBag.Success = "Đăng ký thành công! Vui lòng đăng nhập.";
                return RedirectToAction("SignIn");
            }
            catch (Exception ex)
            {
                Console.WriteLine("===== EXCEPTION REGISTER =====");
                Console.WriteLine(ex.Message);
                ViewBag.Error = "Đã xảy ra lỗi trong quá trình đăng ký.";
                return View(request);
            }
        }
    }
}
