using bookingfootball.Db_QL;
using bookingfootball.Data;

namespace bookingfootball.Interfaces
{
    public interface IUserRepository:IRepositoryBase<TaiKhoan>
    {
        Task<TaiKhoan?> GetUserByEmailAsync(string email, bool asNoTracking = false);

        Task<bool> EmailExistsAsync(string email);
        Task<bool> UserNameExistsAsync(string userName);
        Task<bool> UserExistsAsync(int userId);
    }
}
