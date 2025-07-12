using bookingfootball.Data;
using bookingfootball.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mvc.Models;
using System.Security.Claims;

namespace Mvc.Controllers
{
    public class LichSuController : Controller
    {
        private readonly SbDbcontext _context;
        private readonly ILogger<LichSuController> _logger;

        public LichSuController(SbDbcontext context, ILogger<LichSuController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            _logger.LogInformation("LichSu Index: Starting for page {Page}, pageSize {PageSize}", page, pageSize);

            // Lấy KhachHangId từ session hoặc User.Identity (giả định session)

            if (!User.Identity.IsAuthenticated)
            {
                Console.WriteLine("ConfirmAndBook Error: User not authenticated");
                return Json(new { success = false, message = "Vui lòng đăng nhập để đặt sân." });
            }
            var khachHangId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            try
            {
                // Truy vấn danh sách hóa đơn
                var query = _context.HoaDons
                    .Where(h => h.KhachHangId == khachHangId)
                    .Include(h => h.HoaDonChiTiets)
                        .ThenInclude(hdct => hdct.SanBong)
                    .Include(h => h.HoaDonChiTiets)
                        .ThenInclude(hdct => hdct.SanCa)
                        .ThenInclude(sc => sc.Ca)
                    .Include(h => h.HoaDonChiTiets)
                        .ThenInclude(hdct => hdct.DichVuDatBongs)
                        .ThenInclude(dv => dv.NuocUong)
                    .Include(h => h.HoaDonChiTiets)
                        .ThenInclude(hdct => hdct.DichVuDatBongs)
                        .ThenInclude(dv => dv.Dothue)
                    .Include(h => h.ThoiGianDatSans)
                    .OrderByDescending(h => h.NgayLap);

                // Phân trang
                var totalItems = await query.CountAsync();
                var hoaDons = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var viewModels = hoaDons.Select(h => new LichSuDatSanViewModel
                {
                    HoaDonId = h.Id,
                    MaHoaDon = h.MaHoaDon,
                    NgayLap = h.NgayLap,
                    TongTien = h.TongTien,
                    TienCoc = h.TienCoc,
                    TenNguoiDat = h.TenNguoiDat,
                    Email = h.Email,
                    SoDienThoaiNguoiDat = h.SoDienThoaiNguoiDat,
                    TrangThaiThanhToan = h.TrangThaiThanhToan,
                    TrangThaiHoaDon = h.TrangThaiHoaDon.ToString(),
                    VNPayTransactionId = h.VNPayTransactionId,
                    ChiTietDatSans = h.HoaDonChiTiets.Where(hdct => hdct.IsActive).Select(hdct => new LichSuDatSanViewModel.ChiTietDatSan
                    {
                        HoaDonChiTietId = hdct.Id,
                        MaChiTietHoaDon = hdct.MaChiTietHoaDon,
                        TenSanBong = hdct.SanBong?.TenSan ?? "Không xác định",
                        TenCa = hdct.SanCa?.Ca?.TenCa ?? "Không xác định",
                        StartTime = hdct.SanCa?.Ca?.StartTime ?? DateTime.MinValue,
                        EndTime = hdct.SanCa?.Ca?.EndTime ?? DateTime.MinValue,
                        NgayDenSan = hdct.NgayDenSan,
                        TienThueSan = hdct.TienThueSan ?? 0,
                        TongTienDuocGiam = hdct.TongTienDuocGiam,
                        TongTienChiTiet = hdct.TongTien,
                        GhiChu = hdct.GhiChu,
                        DichVus = hdct.DichVuDatBongs.Where(dv => dv.IsActive).Select(dv => new LichSuDatSanViewModel.DichVuDat
                        {
                            TenDichVu = dv.NuocUong?.TenNuocUong ?? dv.Dothue?.TenDoThue ?? "Không xác định",
                            SoLuong = dv.SoLuong,
                            DonGia = dv.NuocUong?.GiaBan ?? dv.Dothue?.DonGia ?? 0,
                            TongTien = dv.TongTien
                        }).ToList()
                    }).ToList()
                }).ToList();

                // Tạo model cho phân trang
                var model = new PaginatedList<LichSuDatSanViewModel>(viewModels, totalItems, page, pageSize);

                _logger.LogInformation("LichSu Index: Loaded {Count} hóa đơn for KhachHangId {KhachHangId}", viewModels.Count, khachHangId);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "LichSu Index: Error loading lịch sử đặt sân for KhachHangId {KhachHangId}", khachHangId);
                return View("Error", new ErrorViewModel { Message = "Lỗi khi tải lịch sử đặt sân." });
            }
        }
    }

    // Class hỗ trợ phân trang
  
}