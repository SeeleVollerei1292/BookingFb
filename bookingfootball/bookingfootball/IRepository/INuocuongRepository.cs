using bookingfootball.Db_QL;

namespace bookingfootball.IRepository
{
    public interface INuocuongRepository
    {
        Task<IEnumerable<NuocUong>> GetAllNuocUongAsync();
        Task<NuocUong> GetNuocUongById(int id);
        Task CreateNuocUong(NuocUong nuocUong);
        Task UpdateNuocUong(int id,NuocUong nuocUong);
        Task DisableNuocUong(int id);
    }
}
