using bookingfootball.Db_QL;
using Microsoft.EntityFrameworkCore;
using bookingfootball.Data;

namespace bookingfootball.IRepository.Repository
{
    public class NhanVienRepository : INhanVienRepository
    {
        private readonly SbDbcontext _context;
        public NhanVienRepository(SbDbcontext context)
        {
            _context = context;
        }
        public async Task CreateNhanvien(NhanVien nv)
        {
            try
            {
     
                if (nv == null)
                {
                    throw new ArgumentNullException(nameof(nv), "NhanVien cannot be null");
                }
                _context.NhanViens.Add(nv);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("An error occurred while creating NhanVien: " + ex.Message);
            }
        }

        public async Task DisableNhanVien(int id)
        {
            try
            {
                var nv = await GetNhanVienById(id);
                if (nv == null)
                {
                    throw new KeyNotFoundException($"NhanVien with id {id} not found");
                }
                nv.IsActive = false;
                _context.NhanViens.Remove(nv);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("An error occurred while deleting NhanVien: " + ex.Message);
            }
        }

        public async Task<IEnumerable<NhanVien>> GetAllNhanVienAsync()
        {
            return await _context.NhanViens
                .Include(nv => nv.TaiKhoan)
                .ToListAsync();
        }

        public async Task<NhanVien> GetNhanVienById(int id)
        {
            return await _context.NhanViens
                .Include(nv => nv.TaiKhoan)
                .FirstOrDefaultAsync(nv => nv.Id == id);
        }

        public async Task UpdateNhanVien(int id, NhanVien nv)
        {
            try
            {
                var existingNv = await GetNhanVienById(id);
                existingNv.FullName = nv.FullName;
                existingNv.StaffCode = nv.StaffCode;
                existingNv.Username = nv.Username;
                existingNv.IsActive = nv.IsActive;
                existingNv.Email = nv.Email;
                existingNv.PhoneNumber = nv.PhoneNumber;
                existingNv.Address = nv.Address;
                existingNv.IdentityNumber = nv.IdentityNumber;
                existingNv.UpdatedAt = DateTime.UtcNow; 

                _context.NhanViens.Update(existingNv);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new NotImplementedException("An error occurred while updating NhanVien: " + ex.Message);
            }
        }
    }
}
