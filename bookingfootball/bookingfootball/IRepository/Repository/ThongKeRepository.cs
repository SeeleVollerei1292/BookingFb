using bookingfootball.Data;
using bookingfootball.Db_QL;
using bookingfootball.DTOs;
using Microsoft.EntityFrameworkCore;


namespace bookingfootball.IRepository.Repository
{
    public class ThongKeRepository:IThongKeRepository
    {
        private readonly SbDbcontext _context;

        public ThongKeRepository(SbDbcontext context)
        {
            _context = context;
        }
        public async Task<List<DoanhThuTheoSanDTO>> GetDoanhThuTheoSan()
        {
            return await Task.Run(() =>
            {
                // Lấy thông tin từ bảng HoaDonChiTiet và liên kết với HoaDon, LichSuHoaDon, Sanbong
                return _context.HoaDonChiTiets
                    .Join(_context.HoaDons, hdt => hdt.HoaDonId, hd => hd.Id, (hdt, hd) => new { hdt, hd })
                    .Join(_context.LichSuHoaDons, x => x.hd.Id, lshd => lshd.HoaDonId, (x, lshd) => new { x, lshd })
                    .Where(x => x.lshd.IsActive == true)
                    .Join(_context.Sanbongs, x => x.x.hdt.SanBongId, s => s.Id, (x, s) => new { x, s })
                    .GroupBy(x => x.s.TenSan)
                    .Select(g => new DoanhThuTheoSanDTO
                    {
                        TenSan = g.Key,
                        DoanhThu = g.Sum(x => x.x.x.hd.TongTien)
                    })
                    .OrderByDescending(dt => dt.DoanhThu)
                    .ToList();
            });
        }

        public async Task<List<DoanhThuTheoNgayDTO>> GetDoanhThuTheoNgay()
        {
            return await Task.Run(() =>
            {
                return _context.HoaDons
                    .GroupBy(hd => hd.NgayLap.Date)
                    .Select(g => new DoanhThuTheoNgayDTO
                    {
                        Ngay = g.Key,
                        DoanhThu = g.Sum(hd => hd.TongTien)
                    })
                    .OrderByDescending(dt => dt.Ngay)
                    .ToList();
            });
        }

        public async Task<List<DoanhThuTheoThangDTO>> GetDoanhThuTheoThang()
        {
            return await Task.Run(() =>
            {
                return _context.HoaDons
                    .GroupBy(hd => new { hd.NgayLap.Month, hd.NgayLap.Year })
                    .Select(g => new DoanhThuTheoThangDTO
                    {
                        Thang = g.Key.Month,
                        Nam = g.Key.Year,
                        DoanhThu = g.Sum(hd => hd.TongTien)
                    })
                    .OrderByDescending(dt => dt.Nam)
                    .ThenByDescending(dt => dt.Thang)
                    .ToList();
            });
        }

        public async Task<List<DoanhThuTheoNamDTO>> GetDoanhThuTheoNam()
        {
            return await Task.Run(() =>
            {
                return _context.HoaDons
                    .GroupBy(hd => hd.NgayLap.Year)
                    .Select(g => new DoanhThuTheoNamDTO
                    {
                        Nam = g.Key,
                        DoanhThu = g.Sum(hd => hd.TongTien)
                    })
                    .OrderByDescending(dt => dt.Nam)
                    .ToList();
            });
        }

       

        // Thống kê sử dụng sân
        public async Task<ThongKeSuDungSanDTO> GetThongKeSuDungSan()
        {
            return await Task.Run(() =>
            {
                var thongKe = new ThongKeSuDungSanDTO();

                // Thống kê sử dụng sân
                thongKe.SuDungSan = _context.HoaDonChiTiets
                    .Join(_context.Sanbongs, hd => hd.SanBongId, s => s.Id, (hd, s) => new { hd, s })
                    .GroupBy(x => x.s.TenSan)
                    .Select(g => new SuDungSanDTO
                    {
                        TenSan = g.Key,
                        SoLanSuDung = g.Count()
                    })
                    .OrderByDescending(s => s.SoLanSuDung)
                    .ToList();

                // Thống kê thời gian cao điểm
                thongKe.ThoiGianCaoDiem = _context.HoaDonChiTiets
                    .GroupBy(hd => hd.NgayDenSan)
                    .Select(g => new ThoiGianCaoDiemDTO
                    {
                        Gio = g.Key.ToString("HH:mm"),
                        SoLanSuDung = g.Count()
                    })
                    .OrderByDescending(t => t.SoLanSuDung)
                    .ToList();

                return thongKe;
            });
        }

        // Thống kê theo khoảng thời gian
        public async Task<ThongKeDTO> GetFilteredStatistics(DateTime? fromDate, DateTime? toDate)
        {
            return await Task.Run(() =>
            {
                // Lấy danh sách hóa đơn đã thanh toán
                var hoaDonDaThanhToan = _context.HoaDons
                    .Join(_context.LichSuHoaDons, hd => hd.Id, lshd => lshd.HoaDonId, (hd, lshd) => new { hd, lshd })
                    .Where(x => x.lshd.IsActive == true);

                // Áp dụng điều kiện thời gian
                if (fromDate.HasValue)
                {
                    hoaDonDaThanhToan = hoaDonDaThanhToan.Where(x => x.hd.NgayLap >= fromDate.Value);
                }

                if (toDate.HasValue)
                {
                    hoaDonDaThanhToan = hoaDonDaThanhToan.Where(x => x.hd.NgayLap <= toDate.Value);
                }

                // Tạo model thống kê
                var model = new ThongKeDTO();

                // Thống kê doanh thu theo thời gian
                model.DoanhThuTheoThoiGian = hoaDonDaThanhToan
                    .GroupBy(x => x.hd.NgayLap)
                    .Select(g => new DoanhThuTheoThoiGianDTO
                    {
                        ThoiGian = g.Key,
                        DoanhThu = g.Sum(x => x.hd.TongTien)
                    })
                    .OrderByDescending(dt => dt.ThoiGian)
                    .ToList();

                model.TongDoanhThu = model.DoanhThuTheoThoiGian.Sum(dt => dt.DoanhThu);

                // Thống kê doanh thu theo sân
                model.DoanhThuTheoSan = hoaDonDaThanhToan
                    .Join(_context.HoaDonChiTiets, hd => hd.hd.Id, hdt => hdt.HoaDonId, (hd, hdt) => new { hd, hdt })
                    .Join(_context.Sanbongs, x => x.hdt.SanBongId, s => s.Id, (x, s) => new { x, s })
                    .GroupBy(x => x.s.TenSan)
                    .Select(g => new DoanhThuTheoSanDTO
                    {
                        TenSan = g.Key,
                        DoanhThu = g.Sum(x => x.x.hd.hd.TongTien)
                    })
                    .OrderByDescending(dt => dt.DoanhThu)
                    .ToList();

                return model;
            });
        }
    }
}
