using Duong_API.Data;

namespace DuongPia.Areas.Admin.IServices.Services
{
    public class DoThueServices : IDoThueService
    {
        private readonly HttpClient _httpClient;
        public DoThueServices(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7054/");
        }
        public async Task CreateAsync(DoThue doThue)
        {
            var response = await _httpClient.PostAsJsonAsync("api/DoThue", doThue);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/DoThue/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<DoThue>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<DoThue>>("api/DoThue");
        }

        public async Task<DoThue> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<DoThue>($"api/DoThue/{id}");
        }

        public async Task UpdateAsyncs(int id, DoThue doThue)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/DoThue/{id}", doThue);
            response.EnsureSuccessStatusCode();
        }
    }
}
