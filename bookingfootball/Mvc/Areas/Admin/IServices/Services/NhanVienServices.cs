using bookingfootball.Db_QL;

namespace Mvc.Areas.Admin.IServices.Services
{
    public class NhanVienServices : INhanVienServices
    {
        private readonly HttpClient _httpClient;
        public NhanVienServices(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7054/"); 
        }
        public async Task CreateNhanvien(NhanVien nv)
        {
            var response = await _httpClient.PostAsJsonAsync("api/NhanVien", nv);
            response.EnsureSuccessStatusCode();
        }

        public async Task DisableNhanVien(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/NhanVien/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<bookingfootball.Db_QL.NhanVien>> GetAllNhanVienAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<NhanVien>>("api/NhanVien");

        }

        public Task<NhanVien> GetNhanVienById(int id)
        {
            return _httpClient.GetFromJsonAsync<NhanVien>($"api/NhanVien/{id}");
        }

        public async Task UpdateNhanVien(int id, NhanVien nv)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/NhanVien/{id}", nv);
            response.EnsureSuccessStatusCode();
        }
    }
}
