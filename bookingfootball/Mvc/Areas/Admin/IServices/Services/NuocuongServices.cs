using bookingfootball.Db_QL;

namespace Mvc.Areas.Admin.IServices.Services
{
    public class NuocuongServices : INuocuongServices
    {
        private readonly HttpClient _httpClient;

        public NuocuongServices(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7054/"); 
        }

        public async Task CreateNuocUong(NuocUong nuocUong)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Nuocuong", nuocUong);
            response.EnsureSuccessStatusCode();
        }

        public async Task DisableNuocUong(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Nuocuong/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<NuocUong>> GetAllNuocUongAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<NuocUong>>("api/Nuocuong");
        }

        public Task<NuocUong> GetNuocUongById(int id)
        {
             return _httpClient.GetFromJsonAsync<NuocUong>($"api/Nuocuong/{id}");
        }

        public async Task UpdateNuocUong(int id, NuocUong nuocUong)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Nuocuong/{id}", nuocUong);
            response.EnsureSuccessStatusCode();
        }
    }
}
