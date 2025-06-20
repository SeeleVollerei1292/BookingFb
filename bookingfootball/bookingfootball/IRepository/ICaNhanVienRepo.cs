using bookingfootball.DTOs;

namespace bookingfootball.IRepository
{
    public interface ICaNhanVienRepo
    {
        Task<IEnumerable<CaNhanVienDTO>> GetAllAsync();
        Task<CaNhanVienDTO?> GetByIdAsync(int id);
        Task<CaNhanVienDTO> AddAsync(CaNhanVienDTO dto);
        Task<CaNhanVienDTO> UpdateAsync(int id, CaNhanVienDTO dto);
        Task DeleteAsync(int id);
    }

}
