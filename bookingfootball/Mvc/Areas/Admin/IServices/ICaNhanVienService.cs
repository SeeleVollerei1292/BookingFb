using bookingfootball.DTOs;

namespace Mvc.Areas.Admin.IServices
{
    public interface ICaNhanVienService
    {
        Task<IEnumerable<CaNhanVienDTO>> GetAllAsync();
        Task<CaNhanVienDTO?> GetByIdAsync(int id);
        Task AddAsync(CaNhanVienDTO dto);
        Task UpdateAsync(CaNhanVienDTO dto, int id);
        Task DeleteAsync(int id);
    }
}
