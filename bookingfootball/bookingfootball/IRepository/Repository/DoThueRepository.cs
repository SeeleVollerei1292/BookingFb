using bookingfootball.Data;
using Duong_API.Data;
using Microsoft.EntityFrameworkCore;

namespace Duong_API.IRepository.Repository
{
    public class DoThueRepository : IDoThueRepository
    {
        private readonly SbDbcontext _context;
        public DoThueRepository(SbDbcontext context)
        {
            _context = context;
        }
        public async Task CreateAsync(DoThue doThue)
        {
            try
            {
                _context.DoThues.Add(doThue);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while creating the item.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var doThue = await GetByIdAsync(id);
                if (doThue == null)
                {
                    throw new Exception("Item not found.");
                }

                _context.DoThues.Remove(doThue);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while deleting the item.", ex);
            }
        }

        public async Task<IEnumerable<DoThue>> GetAllAsync()
        {
            return await _context.DoThues.ToListAsync();
        }

        public async Task<DoThue> GetByIdAsync(int id)
        {
            return await _context.DoThues.FindAsync(id);
        }

        public async Task UpdateAsync(int id, DoThue doThue)
        {
            try
            {
                var ex = await GetByIdAsync(id);
                
                ex.TenDoThue = doThue.TenDoThue;
                ex.MoTa = doThue.MoTa;
                ex.HinhAnh = doThue.HinhAnh;
                ex.SoLuong = doThue.SoLuong;
                ex.DonGia = doThue.DonGia;
                ex.TrangThai = doThue.TrangThai;

                _context.DoThues.Update(ex);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while updating the item.", ex);
            }
        }
    }
}
