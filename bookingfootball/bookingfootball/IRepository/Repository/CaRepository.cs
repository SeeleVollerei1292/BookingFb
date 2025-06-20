using bookingfootball.Data;
using bookingfootball.Db_QL;
using Microsoft.EntityFrameworkCore;

namespace bookingfootball.IRepository.Repository
{
    public class CaRepository : ICaRepository
    {
        private readonly SbDbcontext _sbDbcontext;
        public CaRepository(SbDbcontext sbDbcontext)
        {
            _sbDbcontext = sbDbcontext;
        }
        public async Task CreateCaAsync(Ca ca)
        {
            try
            {
                _sbDbcontext.Cas.Add(ca);
                await _sbDbcontext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("Đã xảy ra lỗi khi tạo mới Ca: " + ex.Message, ex);
            }
        }

        public async Task DeleteCaAsync(int id)
        {
            try
            {
                var ca = await GetCaByIdAsync(id);
                if (ca == null)
                {
                    throw new Exception("Ca không tồn tại.");
                }
                _sbDbcontext.Cas.Remove(ca);
                await _sbDbcontext.SaveChangesAsync();
            }
            catch
            (Exception ex)
            {
                throw new Exception("Đã xảy ra lỗi khi xóa Ca: " + ex.Message, ex);
            }
        }

        public async Task<Ca> GetCaByIdAsync(int id)
        {
            return await _sbDbcontext.Cas.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Ca>> GetCasAsync()
        {
            return await _sbDbcontext.Cas.ToListAsync();
        }

        public async Task UpdateCaAsync(int id, Ca ca)
        {
            try
            {
                var ae = await GetCaByIdAsync(id);
                if (ae == null)
                {
                    throw new Exception("Ca không tồn tại.");
                }
                ae.TenCa = ca.TenCa;
                ae.EndTime = ca.EndTime;
                ae.StartTime = ca.StartTime;
                ae.IsActive = ca.IsActive;
                _sbDbcontext.Cas.Update(ae);
                await _sbDbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Đã xảy ra lỗi khi cập nhật Ca: " + ex.Message, ex);
            }
        }
    }
}
