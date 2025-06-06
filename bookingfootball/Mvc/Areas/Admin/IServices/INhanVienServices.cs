using bookingfootball.Db_QL;

namespace Mvc.Areas.Admin.IServices
{
    public interface INhanVienServices
    {
        Task<IEnumerable<NhanVien>> GetAllNhanVienAsync();
        Task<NhanVien> GetNhanVienById(int id);
        Task CreateNhanvien(NhanVien nv);
        Task UpdateNhanVien(int id,NhanVien nv);
        Task DisableNhanVien(int id);
    }
}
