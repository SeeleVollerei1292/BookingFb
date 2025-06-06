using bookingfootball.Db_QL;
using bookingfootball.Interfaces;
using bookingfootball.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using bookingfootball.Data;

namespace bookingfootball.Persistence.Repository
{
    public class UserRepository : RepositoryBase<TaiKhoan>, IUserRepository
    {
        public UserRepository(SbDbcontext context) : base(context)
        {
        }
        public async Task<TaiKhoan?> GetUserByEmailAsync(string email, bool asNoTracking = false)
        {
            var query = asNoTracking ? FindAll(true) : FindAll(false);
            return await query
                .Include(u => u.KhachHang)
                .Include(u => u.NhanVien)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await FindByCondition(u => u.Email == email, false).AnyAsync();
        }

        public async Task<bool> UserNameExistsAsync(string userName)
        {
            return await FindByCondition(u => u.TenDangNhap == userName, false).AnyAsync();
        }

        public async Task<bool> UserExistsAsync(int userId)
        {
            return await FindByCondition(u => u.Id == userId, false).AnyAsync();
        }
    }
}
