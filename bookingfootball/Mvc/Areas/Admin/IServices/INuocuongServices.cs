using bookingfootball.Db_QL;

namespace Mvc.Areas.Admin.IServices
{
    public interface INuocuongServices
    {
        Task<IEnumerable<NuocUong>> GetAllNuocUongAsync();
        Task<NuocUong> GetNuocUongById(int id);
        Task CreateNuocUong(NuocUong nuocUong);
        Task UpdateNuocUong(int id,NuocUong nuocUong);
        Task DisableNuocUong(int id);
    }
}
