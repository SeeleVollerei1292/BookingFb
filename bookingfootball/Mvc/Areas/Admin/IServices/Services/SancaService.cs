using bookingfootball.Data;
using bookingfootball.Db_QL;
using bookingfootball.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Mvc.Areas.Admin.IServices.Services
{
    public class SancaService : ISancaService
    {
        private HttpClient _httpClient;
        private readonly SbDbcontext _sbDbcontext;
        public SancaService(IHttpClientFactory httpClient, SbDbcontext sbDbcontext)
        {
            _sbDbcontext = sbDbcontext;
            _httpClient = httpClient.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7054/");

        }

        public async Task CreateSanCaAsync(SanCa sanCa)
        {
            var response = await _httpClient.PostAsJsonAsync("api/SanCa", sanCa);
            response.EnsureSuccessStatusCode();
        }
        public async Task DeleteSanCaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/SanCa/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<Ca>> GetCasAsync()
        {
             return await _httpClient.GetFromJsonAsync<IEnumerable<Ca>>("api/Ca");
        }

        public async Task<IEnumerable<Sanbong>> GetSanBongsAsync()
        {
            return await _sbDbcontext.Sanbongs.Where(s => s.TrangThai).ToListAsync();
        }

        public async Task<SanCa> GetSanCaByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<SanCa>($"api/SanCa/{id}");
        }

        public async Task<IEnumerable<bookingfootball.Db_QL.SanCa>> GetSanCasAsync()
        {
            return await _sbDbcontext.SanCas
            .Include(x => x.Ca)
            .Include(x => x.SanBong)
            .Include(x => x.NgayTrongTuan)
            .ToListAsync();
        }

        public async Task<IEnumerable<NgayTrongTuan>> GetThuTuansAsync()
        {
            return await _sbDbcontext.NgayTrongTuans.Where(n => n.IsActive).ToListAsync();
        }

        public async Task UpdateSanCaAsync(int id, SanCa sanCa)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/SanCa/{id}", sanCa);
            response.EnsureSuccessStatusCode();
        }
    }
}
