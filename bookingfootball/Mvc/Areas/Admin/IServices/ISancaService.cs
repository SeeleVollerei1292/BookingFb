using bookingfootball.Db_QL;

namespace Mvc.Areas.Admin.IServices
{
    public interface ISancaService
    {
		Task<IEnumerable<Ca>> GetCasAsync();
		Task<IEnumerable<Sanbong>> GetSanBongsAsync();
		Task<IEnumerable<NgayTrongTuan>> GetThuTuansAsync();
		Task<IEnumerable<SanCa>> GetSanCasAsync();
        Task<SanCa> GetSanCaByIdAsync(int id);
        Task CreateSanCaAsync(SanCa sanCa);
        Task UpdateSanCaAsync(int id, SanCa sanCa);
        Task DeleteSanCaAsync(int id);
    }
}
