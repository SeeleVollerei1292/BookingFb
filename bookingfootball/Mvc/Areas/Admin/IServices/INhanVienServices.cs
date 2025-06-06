
using NhanVien = Mvc.Areas.Admin.Model.NhanVien;

namespace Mvc.Areas.Admin.IServices
{
    public interface INhanVienServices
    {
        Task<IEnumerable<Model.NhanVien>> GetAllNhanVienAsync();
        Task<NhanVien> GetNhanVienById(int id);
        Task CreateNhanvien(Model.NhanVien nv);
        Task UpdateNhanVien(int id, Model.NhanVien nv);
        Task DisableNhanVien(int id);
    }
}
