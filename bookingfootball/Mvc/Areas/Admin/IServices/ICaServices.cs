using bookingfootball.Db_QL;

namespace Mvc.Areas.Admin.IServices
{
    public interface ICaServices
    {
        Task<IEnumerable<Ca>> GetCasAsync();
        Task<Ca> GetCaByIdAsync(int id);
        Task CreateCaAsync(Ca ca);
        Task UpdateCaAsync(int id, Ca ca);
        Task DeleteCaAsync(int id);
    }
}
