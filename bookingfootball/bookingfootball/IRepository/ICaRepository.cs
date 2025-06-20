using bookingfootball.Db_QL;

namespace bookingfootball.IRepository
{
    public interface ICaRepository
    {
        Task<IEnumerable<Ca>> GetCasAsync();
        Task<Ca> GetCaByIdAsync(int id);
        Task CreateCaAsync(Ca ca);
        Task UpdateCaAsync(int id, Ca ca);
        Task DeleteCaAsync(int id);
    }
}
