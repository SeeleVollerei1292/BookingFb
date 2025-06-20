using bookingfootball.Db_QL;

namespace bookingfootball.IRepository
{
    public interface ISancaRepository
    {
        Task<IEnumerable<SanCa>> GetSanCasAsync();
        Task<SanCa> GetSanCaByIdAsync(int id);
        Task CreateSanCaAsync(SanCa sanCa);
        Task UpdateSanCaAsync(int id, SanCa sanCa);
        Task DeleteSanCaAsync(int id);
    }
}
