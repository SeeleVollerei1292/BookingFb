using bookingfootball.DTOs;
using Mvc.Areas.Admin.IServices;

namespace Mvc.Areas.Admin.IServices.Services
{
    public class CaNhanVienService : ICaNhanVienService
    {
        private readonly HttpClient _httpClient;

        public CaNhanVienService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7054/");
        }

        public async Task<IEnumerable<CaNhanVienDTO>> GetAllAsync() =>
            await _httpClient.GetFromJsonAsync<IEnumerable<CaNhanVienDTO>>("api/CaNhanVien");

        public async Task<CaNhanVienDTO?> GetByIdAsync(int id) =>
            await _httpClient.GetFromJsonAsync<CaNhanVienDTO>($"api/CaNhanVien/{id}");

        public async Task AddAsync(CaNhanVienDTO dto)
        {
            var res = await _httpClient.PostAsJsonAsync("api/CaNhanVien", dto);
            res.EnsureSuccessStatusCode();
        }
        public async Task UpdateAsync(CaNhanVienDTO dto, int id)
        {
            var res = await _httpClient.PutAsJsonAsync($"api/CaNhanVien/{id}", dto);
            res.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var res = await _httpClient.DeleteAsync($"api/CaNhanVien/{id}");
            res.EnsureSuccessStatusCode();
        }
    }
}