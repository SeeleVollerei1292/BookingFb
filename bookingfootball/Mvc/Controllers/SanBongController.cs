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

            decimal tongTien = 0;

            // Lấy danh sách Ca
            var cas = await _context.Cas.Where(c => caIds.Contains(c.Id)).ToListAsync();
            foreach (var ca in cas)
            {
                // Kiểm tra null nếu StartTime hoặc EndTime là nullable
                if (ca.StartTime != null && ca.EndTime != null)
                {
                    var duration = (ca.EndTime - ca.StartTime).TotalHours;
                    tongTien += (decimal)duration * sanbong.Gia;
                }
            }

            // DoThue cost
            var doThueIds = selections
                ?.SelectMany(s => s.DoThueIds ?? new List<int>())
                .Distinct()
                .ToList() ?? new List<int>();

            var doThues = await _context.DoThues
                .Where(dt => doThueIds.Contains(dt.Id))
                .ToListAsync();

            foreach (var dt in doThues)
            {
                tongTien += dt.DonGia;
            }

            // NuocUong cost
            var nuocUongSelections = selections?
                .SelectMany(s => s.NuocUongSelections ?? new List<NuocUongSelection>())
                .GroupBy(nu => nu.Id)
                .ToDictionary(g => g.Key, g => g.Sum(nu => nu.Quantity)) ?? new Dictionary<int, int>();

            var nuocUongs = await _context.NuocUongs
                .Where(nu => nuocUongSelections.Keys.Contains(nu.Id))
                .ToListAsync();

            foreach (var nu in nuocUongs)
            {
                if (nuocUongSelections.TryGetValue(nu.Id, out var quantity))
                {
                    tongTien += nu.GiaBan * quantity;
                }
            }

            // Create HoaDon
            var hoaDon = new HoaDon
            {
                KhachHangId = khachHangId,
                NhanVienId = null,
                MaHoaDon = $"HD-{DateTime.Now:yyyyMMddHHmmss}",
                NgayLap = DateTime.Now,
                TongTien = tongTien,
                TongTienThanhToan = tongTien,
                GhiChu = ghiChu,
                LichSuHoaDons = new List<LichSuHoaDon>(),
                HoaDonChiTiets = new List<HoaDonChiTiet>()
            };

            // Create HoaDonChiTiet
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
                DichVuDatBongs = new List<DichVuDatBong>(),
                DoThues = doThues
            };

            // Create DichVuDatBong for NuocUong
            foreach (var nu in nuocUongSelections)
            {
                var dichVu = new DichVuDatBong
                {
                    NuocUongId = nu.Key,
                    SoLuong = nu.Value,
                    TongTien = nuocUongs.First(n => n.Id == nu.Key).GiaBan * nu.Value,
                    NgayDat = DateTime.Now,
                    GhiChu = ghiChu,
                    HoaDonChiTiet = hoaDonChiTiet
                };
                hoaDonChiTiet.DichVuDatBongs.Add(dichVu);
            }

            hoaDon.HoaDonChiTiets.Add(hoaDonChiTiet);
            _context.HoaDons.Add(hoaDon);
            await _context.SaveChangesAsync();

            // Structure booking data for confirmation page
            var bookingData = new
            {
                HoaDonId = hoaDon.Id,
                SanBongId = sanbong.Id,
                TenSan = sanbong.TenSan,
                Gia = sanbong.Gia,
                Date = date,
                Cas = cas.Select(c => new
                {
                    c.Id,
                    c.TenCa,
                    StartTime = c.StartTime.ToString("HH:mm"),
                    EndTime = c.EndTime.ToString("HH:mm"),
                    DoThues = selections.Where(s => s.CaId == c.Id)
                        .SelectMany(s => s.DoThueIds)
                        .Distinct()
                        .Join(doThues, id => id, dt => dt.Id, (id, dt) => new
                        {
                            dt.Id,
                            dt.TenDoThue,
                            dt.DonGia
                        }),
                    NuocUongs = selections.Where(s => s.CaId == c.Id)
                        .SelectMany(s => s.NuocUongSelections)
                        .GroupBy(nu => nu.Id)
                        .Select(g => new
                        {
                            Id = g.Key,
                            TenNuocUong = nuocUongs.First(nu => nu.Id == g.Key).TenNuocUong,
                            GiaBan = nuocUongs.First(nu => nu.Id == g.Key).GiaBan,
                            Quantity = g.Sum(nu => nu.Quantity)
                        })
                }),
                GhiChu = ghiChu,
                TongTien = tongTien
            };
            //TempData["ConfirmedBooking"] = JsonSerializer.Serialize(bookingData);

            return Json(new { success = true, message = "Đặt sân thành công!", redirectUrl = Url.Action("ConfirmBooking") });
        }

        [HttpGet]
        public IActionResult ConfirmBooking()
        {
            if (TempData["ConfirmedBooking"] == null)
                return RedirectToAction("Index");

            var json = TempData["ConfirmedBooking"]?.ToString();

            if (!string.IsNullOrEmpty(json))
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                try
                {
                    var booking = JsonSerializer.Deserialize<CaDatSanViewModel>(json, options);
                    return View(booking);
                }
                catch
                {
                    // log lỗi hoặc xử lý tùy ý
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }


        public class BookingSelection
        {
            public int CaId { get; set; }
            public List<int> DoThueIds { get; set; }
            public List<NuocUongSelection> NuocUongSelections { get; set; }
        }

        public class NuocUongSelection
        {
            public int Id { get; set; }
            public int Quantity { get; set; }
        }
    }

    public class CaDatSanViewModel
    {
        public string TenSan { get; set; }
        public int Gia { get; set; }
        public DateTime Date { get; set; }
        public decimal TongTien { get; set; }
        public string? GhiChu { get; set; }
        public string? HoaDonId { get; set; }
        public List<CaDetail> Cas { get; set; }
    }

    public class CaDetail
    {
        public string TenCa { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public List<DoThueDetail>? DoThues { get; set; }
        public List<NuocUongDetail>? NuocUongs { get; set; }
    }

    public class DoThueDetail
    {
        public string TenDoThue { get; set; }
        public int DonGia { get; set; }
    }

    public class NuocUongDetail
    {
        public string TenNuocUong { get; set; }
        public int Quantity { get; set; }
        public decimal GiaBan { get; set; }
    }
}
