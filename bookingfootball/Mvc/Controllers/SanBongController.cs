using bookingfootball.Data;
using bookingfootball.Db_QL;
using Duong_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mvc.Controllers
{
    
    public class SanBongController : Controller
    {
        private readonly SbDbcontext _context;
        private const int PageSize = 9;

        public SanBongController(SbDbcontext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var totalItems = await _context.Sanbongs.Where(s => s.TrangThai).CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / PageSize);
            if (totalPages == 0) totalPages = 1;

            page = page < 1 ? 1 : page > totalPages ? totalPages : page;

            var sanbongs = await _context.Sanbongs
                .Where(s => s.TrangThai)
                .Include(s => s.LoaiSan)
                .OrderBy(s => s.TenSan)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalItems = totalItems;

            return View(sanbongs);
        }

        public async Task<IActionResult> Details(int id)
        {
            var sanbong = await _context.Sanbongs
                .Include(s => s.LoaiSan)
                .Include(s => s.SanCas).ThenInclude(sc => sc.Ca)
                .FirstOrDefaultAsync(s => s.Id == id && s.TrangThai);

            if (sanbong == null)
            {
                return NotFound();
            }

            var cas = await _context.Cas.Where(c => c.IsActive).OrderBy(c => c.StartTime).ToListAsync();
            var doThues = await _context.DoThues.Where(dt => dt.TrangThai).OrderBy(dt => dt.TenDoThue).ToListAsync();
            var nuocUongs = await _context.NuocUongs.Where(nu => nu.IsActive).OrderBy(nu => nu.TenNuocUong).ToListAsync();
            var activeSanCaCount = sanbong.SanCas?.Count(sc => sc.IsActive) ?? 0;

            ViewBag.ActiveSanCaCount = activeSanCaCount;
            ViewBag.Cas = cas;
            ViewBag.DoThues = doThues;
            ViewBag.NuocUongs = nuocUongs;

            return View(sanbong);
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableSlots(int sanBongId, DateTime date)
        {
            int dayOfWeek = ((int)date.DayOfWeek + 6) % 7 + 1;

            var availableSlots = await _context.SanCas
                .Include(sc => sc.Ca)
                .Include(sc => sc.NgayTrongTuan)
                .Where(sc => sc.SanBongId == sanBongId
                    && sc.NgayTrongTuan.ThuTu == dayOfWeek
                    && sc.IsActive
                    && sc.Ca.IsActive
                    && sc.NgayTrongTuan.IsActive)
                .Select(sc => new
                {
                    sc.CaId,
                    sc.Ca.TenCa,
                    StartTime = sc.Ca.StartTime.ToString("HH:mm"),
                    EndTime = sc.Ca.EndTime.ToString("HH:mm")
                })
                .ToListAsync();

            return Json(availableSlots);
        }

        [HttpPost]
        public IActionResult BookSlots(int sanBongId, DateTime date, List<int> caIds)
        {
            if (!caIds.Any())
            {
                return Json(new { success = false, message = "Vui lòng chọn ít nhất một ca." });
            }

            var sanbong = _context.Sanbongs
                .Include(s => s.LoaiSan)
                .FirstOrDefault(s => s.Id == sanBongId && s.TrangThai);

            if (sanbong == null)
            {
                return Json(new { success = false, message = "Sân bóng không tồn tại hoặc không hoạt động." });
            }

            var selectedCas = _context.Cas
                .Where(c => caIds.Contains(c.Id) && c.IsActive)
                .Select(c => new
                {
                    CaId = c.Id,
                    c.TenCa,
                    StartTime = c.StartTime.ToString("HH:mm"),
                    EndTime = c.EndTime.ToString("HH:mm")
                })
                .ToList();

            var bookingData = new
            {
                SanBongId = sanbong.Id,
                TenSan = sanbong.TenSan,
                Gia = sanbong.Gia,
                Date = date.ToString("dd/MM/yyyy"),
                Cas = selectedCas
            };
            TempData["BookingData"] = JsonSerializer.Serialize(bookingData);

            return Json(new
            {
                success = true,
                message = "Chọn ca thành công!",
                data = bookingData
            });
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmAndBook(int sanBongId, string date, List<int> caIds, List<BookingSelection> selections, string ghiChu)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { success = false, message = "Vui lòng đăng nhập để đặt sân." });
            }

            if (!caIds.Any())
            {
                return Json(new { success = false, message = "Vui lòng chọn ít nhất một ca." });
            }

            var khachHangId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var sanbong = await _context.Sanbongs.FindAsync(sanBongId);
            if (sanbong == null || !sanbong.TrangThai)
            {
                return Json(new { success = false, message = "Sân bóng không tồn tại hoặc không hoạt động." });
            }

            // Kiểm tra tính khả dụng của ca
            var cas = await _context.Cas
                .Where(c => caIds.Contains(c.Id) && c.IsActive)
                .ToListAsync();
            if (!cas.Any())
            {
                return Json(new { success = false, message = "Không có ca hợp lệ được chọn." });
            }

            decimal tongTien = 0;
            // Tính chi phí thuê sân
            foreach (var ca in cas)
            {
                var duration = (ca.EndTime - ca.StartTime).TotalHours;
                if (duration > 0)
                {
                    tongTien += (decimal)duration * sanbong.Gia;
                }
            }

            // Xử lý dịch vụ
            var doThueSelections = (selections ?? new List<BookingSelection>())
                .SelectMany(s => (s.DoThueIds ?? new List<DoThueSelection>()).Select(dt => new
                {
                    s.CaId,
                    DoThueId = dt.Id,
                    SoLuongDoThue = dt.Quantity
                }))
                .Where(dt => dt.SoLuongDoThue > 0)
                .Distinct()
                .ToList();

            var nuocUongSelections = (selections ?? new List<BookingSelection>())
                .SelectMany(s => (s.NuocUongSelections ?? new List<NuocUongSelection>()).Select(nu => new
                {
                    s.CaId,
                    NuocUongId = nu.Id,
                    SoLuong = nu.Quantity
                }))
                .Where(nu => nu.SoLuong > 0)
                .GroupBy(nu => new { nu.CaId, nu.NuocUongId })
                .Select(g => new
                {
                    g.Key.CaId,
                    g.Key.NuocUongId,
                    SoLuong = g.Sum(nu => nu.SoLuong)
                })
                .ToList();

            // Lấy danh sách hợp lệ
            var validDoThueIds = doThueSelections.Select(s => s.DoThueId).Distinct().ToList();
            var validNuocUongIds = nuocUongSelections.Select(s => s.NuocUongId).Distinct().ToList();
            var doThues = await _context.DoThues
                .Where(dt => validDoThueIds.Contains(dt.Id) && dt.TrangThai)
                .ToListAsync();
            var nuocUongs = await _context.NuocUongs
                .Where(nu => validNuocUongIds.Contains(nu.Id) && nu.IsActive)
                .ToListAsync();

            // Nhóm dịch vụ theo CaId
            var dichVuGroups = (from sel in selections ?? new List<BookingSelection>()
                                let doThueItems = doThueSelections.Where(ds => ds.CaId == sel.CaId).ToList()
                                let nuocUongItems = nuocUongSelections.Where(ns => ns.CaId == sel.CaId).ToList()
                                from dt in doThueItems.DefaultIfEmpty()
                                from nu in nuocUongItems.DefaultIfEmpty()
                                where dt != null || nu != null
                                select new
                                {
                                    CaId = sel.CaId,
                                    DoThueId = dt?.DoThueId,
                                    SoLuongDoThue = dt?.SoLuongDoThue ?? 0,
                                    NuocUongId = nu?.NuocUongId,
                                    SoLuongNuocUong = nu?.SoLuong ?? 0
                                })
                                .GroupBy(x => new { x.CaId, x.DoThueId, x.NuocUongId })
                                .Select(g => new
                                {
                                    g.Key.CaId,
                                    g.Key.DoThueId,
                                    SoLuongDoThue = g.Sum(x => x.SoLuongDoThue),
                                    g.Key.NuocUongId,
                                    SoLuongNuocUong = g.Sum(x => x.SoLuongNuocUong)
                                })
                                .Where(x => x.SoLuongDoThue > 0 || x.SoLuongNuocUong > 0)
                                .ToList();

            decimal dichVuCost = 0;
            var dichVuDatBongs = new List<DichVuDatBong>();
            foreach (var group in dichVuGroups)
            {
                decimal groupCost = 0;
                var doThue = group.DoThueId.HasValue ? doThues.FirstOrDefault(dt => dt.Id == group.DoThueId.Value) : null;
                var nuocUong = group.NuocUongId.HasValue ? nuocUongs.FirstOrDefault(nu => nu.Id == group.NuocUongId.Value) : null;

                if (doThue != null)
                {
                    groupCost += doThue.DonGia * group.SoLuongDoThue;
                }
                if (nuocUong != null)
                {
                    groupCost += nuocUong.GiaBan * group.SoLuongNuocUong;
                }

                if (groupCost > 0)
                {
                    var dichVu = new DichVuDatBong
                    {
                        NuocUongId = group.NuocUongId ?? 0, // Sửa thành null nếu model cho phép
                        DoThueId = group.DoThueId,
                        SoLuong = group.SoLuongNuocUong,
                        SoLuongDoThue = group.SoLuongDoThue,
                        TongTien = groupCost,
                        NgayDat = DateTime.Now,
                        GhiChu = ghiChu ?? string.Empty,
                        IsActive = true
                    };
                    dichVuDatBongs.Add(dichVu);
                    dichVuCost += groupCost;
                }
            }
            tongTien += dichVuCost;

            // Tạo HoaDon
            var hoaDon = new HoaDon
            {
                KhachHangId = khachHangId,
                NhanVienId = null,
                MaHoaDon = $"HD-{DateTime.Now:yyyyMMddHHmmss}",
                NgayLap = DateTime.Now,
                TongTien = tongTien,
                TongTienThanhToan = tongTien,
                GhiChu = ghiChu ?? string.Empty,
                LichSuHoaDons = new List<LichSuHoaDon>(),
                HoaDonChiTiets = new List<HoaDonChiTiet>()
            };

            // Tạo HoaDonChiTiet
            var hoaDonChiTiet = new HoaDonChiTiet
            {
                HoaDon = hoaDon,
                SanBongId = sanBongId,
                NhanVienId = null,
                MaChiTietHoaDon = $"HDCT-{DateTime.Now:yyyyMMddHHmmss}",
                TongTien = tongTien,
                TienThueSan = sanbong.Gia,
                NgayDenSan = DateTime.ParseExact(date, "dd/MM/yyyy", null),
                GhiChu = ghiChu,
                DichVuDatBongs = dichVuDatBongs,
                DoThues = doThues
            };

            // Gán HoaDonChiTietId
            foreach (var dichVu in dichVuDatBongs)
            {
                dichVu.HoaDonChiTiet = hoaDonChiTiet;
            }

            // Transaction
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                hoaDon.HoaDonChiTiets.Add(hoaDonChiTiet);
                _context.HoaDons.Add(hoaDon);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Lỗi khi lưu dữ liệu: {ex.Message}");
                return Json(new { success = false, message = "Đã xảy ra lỗi khi lưu dữ liệu đặt sân: " + ex.Message });
            }

            return Json(new
            {
                success = true,
                message = "Đặt sân thành công!",
                redirectUrl = Url.Action("Index")
            });
        }
        public class BookingSelection
        {
            public int CaId { get; set; }
            public List<DoThueSelection> DoThueIds { get; set; }
            public List<NuocUongSelection> NuocUongSelections { get; set; }
        }

        public class DoThueSelection
        {
            public int Id { get; set; }
            public int Quantity { get; set; }
        }

        public class NuocUongSelection
        {
            public int Id { get; set; }
            public int Quantity { get; set; }
        }
    }
}