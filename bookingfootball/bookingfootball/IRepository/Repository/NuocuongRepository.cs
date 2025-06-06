using bookingfootball.Db_QL;
using Microsoft.EntityFrameworkCore;
using bookingfootball.Data;
using System.Diagnostics;

namespace bookingfootball.IRepository.Repository
{
    public class NuocuongRepository : INuocuongRepository
    {
        private readonly SbDbcontext _sbDbcontext;
        public NuocuongRepository(SbDbcontext sbDbcontext)
        {
            _sbDbcontext = sbDbcontext;
        }
        public async Task CreateNuocUong(NuocUong nuocUong)
        {
            try
            {
                _sbDbcontext.NuocUongs.Add(nuocUong);
                await _sbDbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("An error occurred while creating NuocUong: " + ex.Message);
            }
        }

        public async Task DisableNuocUong(int id)
        {
            try
            {
                var nuocUong = await _sbDbcontext.NuocUongs.FindAsync(id);
                if (nuocUong == null)
                {
                    throw new KeyNotFoundException($"NuocUong with id {id} not found");
                }
                nuocUong.IsActive = false; 
                _sbDbcontext.NuocUongs.Update(nuocUong);
                await _sbDbcontext.SaveChangesAsync();
            } catch
            (Exception ex)
            {
                throw new NotImplementedException("An error occurred while disabling NuocUong: " + ex.Message);
            }
        }

        public async Task<IEnumerable<NuocUong>> GetAllNuocUongAsync()
        {
            return await _sbDbcontext.NuocUongs.ToListAsync();
        }

        public async Task<NuocUong> GetNuocUongById(int id)
        {
            return await _sbDbcontext.NuocUongs.FindAsync(id);
        }

        public async Task UpdateNuocUong(int id, NuocUong nuocUong)
        {
            try
            {
                var existingNuocUong = await GetNuocUongById(id);  
                if (existingNuocUong == null)
                {
                    throw new KeyNotFoundException($"NuocUong with id {id} not found");
                }
                existingNuocUong.TenNuocUong = nuocUong.TenNuocUong;
                existingNuocUong.Soluong = nuocUong.Soluong;
                existingNuocUong.Anh = nuocUong.Anh;
                existingNuocUong.GiaBan = nuocUong.GiaBan;
                existingNuocUong.GhiChu = nuocUong.GhiChu;
                existingNuocUong.IsActive = nuocUong.IsActive;
                _sbDbcontext.NuocUongs.Update(existingNuocUong);
                await _sbDbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("An error occurred while updating NuocUong: " + ex.Message);
            }
        }
    }
}
