using bookingfootball.Db_QL;

namespace Mvc.Areas.Admin.IServices.Services
{
    public class CaServices : ICaServices
    {
        private HttpClient _httpClient;
        public CaServices(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7054/");
        }
        public async Task CreateCaAsync(Ca ca)
        {
			var response = await _httpClient.PostAsJsonAsync("api/Ca", ca);
		    response.EnsureSuccessStatusCode();
		}

        public async Task DeleteCaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Ca/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<Ca> GetCaByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Ca>($"api/Ca/{id}"); 
        }

        public async Task<IEnumerable<Ca>> GetCasAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Ca>>("api/Ca"); 
        }

        public async Task UpdateCaAsync(int id,Ca ca)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Ca/{id}", ca);
            response.EnsureSuccessStatusCode();
        }
    }
}
