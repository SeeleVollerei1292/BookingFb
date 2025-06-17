using bookingfootball.Data;
using bookingfootball.Db_QL;
using Microsoft.EntityFrameworkCore;

namespace bookingfootball.IRepository.Repository
{
    public class SancaRepository : ISancaRepository
    {
        private readonly SbDbcontext _context;

        public SancaRepository(SbDbcontext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SanCa>> GetSanCasAsync()
        {
            var query = from sanca in _context.SanCas
                        join san in _context.Sanbongs on sanca.SanBongId equals san.Id
                        join ca in _context.Cas on sanca.CaId equals ca.Id
                        join thu in _context.NgayTrongTuans on sanca.NgayTrongTuanId equals thu.Id
                        select new SanCa
                        {
                            Id = sanca.Id,
                            SanBongId = san.Id,
                            CaId = ca.Id,
                            NgayTrongTuanId = thu.Id,
                            IsActive = sanca.IsActive
                        };

            return await query.ToListAsync();
        }

        public async Task<SanCa> GetSanCaByIdAsync(int id)
        {
            return await _context.SanCas
                                 .Include(a => a.SanBong)
                                 .Include(b => b.Ca)
                                 .Include(c => c.NgayTrongTuan)
                                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateSanCaAsync(SanCa sanCa)
        {
            try
            {
                // Kiểm tra khóa ngoại
                if (!await _context.Sanbongs.AnyAsync(s => s.Id == sanCa.SanBongId))
                    throw new Exception("SanBongId không tồn tại.");
                if (!await _context.Cas.AnyAsync(c => c.Id == sanCa.CaId))
                    throw new Exception("CaId không tồn tại.");
                if (!await _context.NgayTrongTuans.AnyAsync(t => t.Id == sanCa.NgayTrongTuanId))
                    throw new Exception("NgayTrongTuanId không tồn tại.");

                _context.SanCas.Add(sanCa);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Đã xảy ra lỗi khi tạo mới SanCa: " + ex.Message, ex);
            }
        }

        public async Task UpdateSanCaAsync(int id, SanCa sanCa)
        {
            try
            {
                var existingSanCa = await _context.SanCas.FindAsync(id);
                if (existingSanCa == null)
                    throw new Exception("Không tìm thấy SanCa.");
                if (!await _context.Sanbongs.AnyAsync(s => s.Id == sanCa.SanBongId))
                    throw new Exception("SanBongId không tồn tại.");
                if (!await _context.Cas.AnyAsync(c => c.Id == sanCa.CaId))
                    throw new Exception("CaId không tồn tại.");
                if (!await _context.NgayTrongTuans.AnyAsync(t => t.Id == sanCa.NgayTrongTuanId))
                    throw new Exception("NgayTrongTuanId không tồn tại.");

                existingSanCa.SanBongId = sanCa.SanBongId;
                existingSanCa.CaId = sanCa.CaId;
                existingSanCa.NgayTrongTuanId = sanCa.NgayTrongTuanId;
                existingSanCa.IsActive = sanCa.IsActive;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Đã xảy ra lỗi khi cập nhật SanCa: " + ex.Message, ex);
            }
        }

        public async Task DeleteSanCaAsync(int id)
        {
            try
            {
                var sanCa = await _context.SanCas.FindAsync(id);
                if (sanCa == null)
                    throw new Exception("Không tìm thấy SanCa để xóa.");

                _context.SanCas.Remove(sanCa);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Đã xảy ra lỗi khi xóa SanCa: " + ex.Message, ex);
            }
        }
    }
}