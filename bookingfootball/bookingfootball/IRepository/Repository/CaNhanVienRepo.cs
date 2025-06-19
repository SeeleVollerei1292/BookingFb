using bookingfootball.Data;
using bookingfootball.Db_QL;
using bookingfootball.DTOs;
using bookingfootball.IRepository;
using Microsoft.EntityFrameworkCore;

namespace bookingfootball.Repository
{
    public class CaNhanVienRepo : ICaNhanVienRepo
    {
        private readonly SbDbcontext _context;

        public CaNhanVienRepo(SbDbcontext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CaNhanVienDTO>> GetAllAsync()
        {
            return await _context.LichLamViecs
                .Include(x => x.NhanVien)
                .Select(x => new CaNhanVienDTO
                {
                    Id = x.Id,
                    NhanVienId = x.NhanVienId,
                    TenNhanVien = x.NhanVien.FullName,
                    ViTri = x.ViTri,
                    GhiChu = x.GhiChu,
                    Ngay = x.Ngay,
                    ThoiGianBatDau = x.ThoiGianBatDau,
                    ThoiGianKetThuc = x.ThoiGianKetThuc
                }).ToListAsync();
        }

        public async Task<CaNhanVienDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.LichLamViecs
                .Include(x => x.NhanVien)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null) return null;

            return new CaNhanVienDTO
            {
                Id = entity.Id,
                NhanVienId = entity.NhanVienId,
                TenNhanVien = entity.NhanVien?.FullName,
                ViTri = entity.ViTri,
                GhiChu = entity.GhiChu,
                Ngay = entity.Ngay,
                ThoiGianBatDau = entity.ThoiGianBatDau,
                ThoiGianKetThuc = entity.ThoiGianKetThuc
            };
        }

        public async Task<CaNhanVienDTO> AddAsync(CaNhanVienDTO dto)
        {
            var nhanVien = await _context.NhanViens.FindAsync(dto.NhanVienId);
            if (nhanVien == null) throw new Exception("Không tìm thấy nhân viên");

            if (string.IsNullOrEmpty(dto.ViTri))
                throw new Exception("Vị trí làm việc không được để trống");

            var existingSchedules = await _context.LichLamViecs
            .Where(x => x.NhanVienId == dto.NhanVienId && x.Ngay.Date == dto.Ngay.Date)
            .ToListAsync();

            // Kiểm tra giao nhau thời gian
            foreach (var s in existingSchedules)
            {
                if (
                    (dto.ThoiGianBatDau < s.ThoiGianKetThuc && dto.ThoiGianKetThuc > s.ThoiGianBatDau)
                )
                {
                    throw new Exception("Ca làm trùng thời gian với một ca khác trong cùng ngày.");
                }
            }

            var entity = new LichLamViec
            {
                NhanVienId = dto.NhanVienId,
                ViTri = dto.ViTri,
                Ngay = dto.Ngay,
                ThoiGianBatDau = dto.ThoiGianBatDau,
                ThoiGianKetThuc = dto.ThoiGianKetThuc,
                GhiChu = dto.GhiChu
            };

            _context.LichLamViecs.Add(entity);
            await _context.SaveChangesAsync();

            var tenNhanVien = await _context.NhanViens
                .Where(nv => nv.Id == dto.NhanVienId)
                .Select(nv => nv.FullName)
                .FirstOrDefaultAsync();

            dto.Id = entity.Id;
            dto.Ngay = entity.Ngay;
            dto.ThoiGianBatDau = entity.ThoiGianBatDau;
            dto.ThoiGianKetThuc = entity.ThoiGianKetThuc;
            dto.TenNhanVien = tenNhanVien;

            return dto;
        }


        public async Task<CaNhanVienDTO> UpdateAsync(int id, CaNhanVienDTO dto)
        {
            var entity = await _context.LichLamViecs.FindAsync(id);
            if (entity == null) throw new Exception("Không tìm thấy lịch làm việc");

            var existingSchedules = await _context.LichLamViecs
           .Where(x => x.NhanVienId == dto.NhanVienId && x.Ngay.Date == dto.Ngay.Date)
           .ToListAsync();

            // Kiểm tra giao nhau thời gian
            foreach (var s in existingSchedules)
            {
                if (
                    (dto.ThoiGianBatDau < s.ThoiGianKetThuc && dto.ThoiGianKetThuc > s.ThoiGianBatDau)
                )
                {
                    throw new Exception("Ca làm trùng thời gian với một ca khác trong cùng ngày.");
                }
            }

            entity.NhanVienId = dto.NhanVienId;
            entity.ViTri = dto.ViTri;
            entity.Ngay = dto.Ngay;
            entity.ThoiGianBatDau = dto.ThoiGianBatDau;
            entity.ThoiGianKetThuc = dto.ThoiGianKetThuc;
            entity.GhiChu = dto.GhiChu;

            await _context.SaveChangesAsync();
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.LichLamViecs.FindAsync(id);
            if (entity == null) 
                throw new KeyNotFoundException($"Lịch làm việc id {id} không tìm thấy");
            _context.LichLamViecs.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}