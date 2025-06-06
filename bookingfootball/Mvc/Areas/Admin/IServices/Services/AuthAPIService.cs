using bookingfootball.Db_QL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System;
using Mvc.Models;
using Mvc.Request;

namespace Mvc.Areas.Admin.IServices.Services
{
    public class AuthAPIService : IAuthAPIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public AuthAPIService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiBaseUrl"] ?? "https://localhost:7054";
        }

        public async Task<Token> LoginAsync(LoginRequest request)
        {
            var apiUrl = $"{_baseUrl}/api/auth/login";
            var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiUrl, jsonContent);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Login failed");
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Token>(content);
        }

        public async Task<Token> RegisterAsync(RegisterRequest request)
        {
            var apiUrl = $"{_baseUrl}/api/auth/register";
            var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiUrl, jsonContent);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Registration failed");
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Token>(content);
        }

        public async Task<CurrentUserResponse> GetCurrentUserAsync()
        {
            var apiUrl = $"{_baseUrl}/api/auth/me";
            var response = await _httpClient.GetAsync(apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to get current user");
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CurrentUserResponse>(content);
        }
    }
}
