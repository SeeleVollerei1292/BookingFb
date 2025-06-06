using bookingfootball.DTOs;
using bookingfootball.IRepository;
using bookingfootball.IRepository.Repository;
using Mvc.Models;
using Newtonsoft.Json;

namespace Mvc.Areas.Admin.IService.Service 
{
    public class ThongKeService : IThongKeService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ThongKeService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ThongKeViewModel> GetThongKeAsync()
        {
            var client = _httpClientFactory.CreateClient("BookingApi");

            // Lấy token từ session
            var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var model = new ThongKeViewModel
            {
                DoanhThuNgay = await GetThongKeAsync<List<DoanhThuTheoNgayDTO>>("/api/thongke/doanh-thu-ngay"),
                DoanhThuThang = await GetThongKeAsync<List<DoanhThuTheoThangDTO>>("/api/thongke/doanh-thu-thang"),
                DoanhThuNam = await GetThongKeAsync<List<DoanhThuTheoNamDTO>>("/api/thongke/doanh-thu-nam"),
                DoanhThuTheoSan = await GetThongKeAsync<List<DoanhThuTheoSanDTO>>("/api/thongke/doanh-thu-theo-san"),
                ThongKeSuDungSan = await GetThongKeAsync<ThongKeSuDungSanDTO>("/api/thongke/su-dung-san")
            };

            return model;
        }

        public async Task<ThongKeDTO> FilterStatisticsAsync(DateTime? fromDate, DateTime? toDate)
        {
            var client = _httpClientFactory.CreateClient("BookingApi");

            var token = _httpContextAccessor.HttpContext?.Session.GetString("Token");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var content = new StringContent(JsonConvert.SerializeObject(new
            {
                FromDate = fromDate,
                ToDate = toDate
            }), System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/thongke/loc-thong-ke", content);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ThongKeDTO>(result);
            }

            return null;
        }

        private async Task<T> GetThongKeAsync<T>(string endpoint)
        {
            var response = await _httpClientFactory.CreateClient("BookingApi").GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }

            return default(T);
        }
    }
}
