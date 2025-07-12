using bookingfootball.Data;

using bookingfootball.Db_QL;

using Duong_API.Data;

using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

using Mvc.Models;

using NuGet.Packaging;

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

            var nuocUongs = await _context.NuocUongs.Where(nu => nu.IsActive).OrderBy(nu => nu.TenNuocUong)

              .ToListAsync();

            var activeSanCaCount = sanbong.SanCas?.Count(sc => sc.IsActive) ?? 0;



            ViewBag.ActiveSanCaCount = activeSanCaCount;

            ViewBag.Cas = cas;

            ViewBag.DoThues = doThues;

            ViewBag.NuocUongs = nuocUongs;



            return View(sanbong);

        }





        [HttpGet]

        public async Task<IActionResult> GetAvailableSlots(int sanBongId, DateTime date, string filter = "day")

        {

            Console.WriteLine($"GetAvailableSlots Input: sanBongId={sanBongId}, date={date:yyyy-MM-dd}, filter={filter}");

            var slots = await _context.SanCas

              .Include(sc => sc.Ca)

              .Include(sc => sc.NgayTrongTuan)

              .Where(sc => sc.SanBongId == sanBongId

                    && sc.NgayTrongTuan.ThuTu == ((int)date.DayOfWeek + 6) % 7 + 1

                    && sc.Ca.IsActive

                    && sc.NgayTrongTuan.IsActive

                    && sc.IsActive)

              .Select(sc => new

              {

                  caId = sc.CaId,

                  sanCaId = sc.Id,

                  tenCa = sc.Ca.TenCa,

                  startTime = sc.Ca.StartTime.ToString("HH:mm"),

                  endTime = sc.Ca.EndTime.ToString("HH:mm"),

                  isActive = !_context.HoaDonChiTiets.Any(hdct => hdct.SanCaId == sc.Id

                                    && hdct.NgayDenSan.Date == date.Date

                                    && hdct.IsActive)

                     && !_context.ThoiGianDatSans.Any(tds => tds.IdSanCa == sc.Id

                                         && tds.NgayDat.Date == date.Date

                                         && tds.SanCa.IsActive),

                  date = date.ToString("yyyy-MM-dd")

              })

              .ToListAsync();



            Console.WriteLine($"GetAvailableSlots Result: {JsonSerializer.Serialize(slots)}");

            return Json(slots);

        }


        [HttpPost]
        public async Task<IActionResult> ConfirmAndBook([FromBody] ConfirmAndBookRequest request)
        {
            Console.WriteLine($"ConfirmAndBook Input: {JsonSerializer.Serialize(request)}");

            if (!User.Identity.IsAuthenticated)
            {
                Console.WriteLine("ConfirmAndBook Error: User not authenticated");
                return Json(new { success = false, message = "Vui lòng đăng nhập để đặt sân." });
            }

            if (request.CaIds == null || !request.CaIds.Any() ||
                request.SanCaIds == null || !request.SanCaIds.Any() ||
                request.Dates == null || !request.Dates.Any() ||
                request.CaIds.Count != request.SanCaIds.Count)
            {
                Console.WriteLine($"ConfirmAndBook Validation Failed: caIds.Any={request.CaIds?.Any()}, sanCaIds.Any={request.SanCaIds?.Any()}, dates.Any={request.Dates?.Any()}, caIds.Count={request.CaIds?.Count}, sanCaIds.Count={request.SanCaIds?.Count}");
                return Json(new { success = false, message = "Vui lòng chọn ít nhất một ca, một SanCa và một ngày, và đảm bảo số lượng CaIds khớp với SanCaIds." });
            }

            var khachHangId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var sanbong = await _context.Sanbongs
                .Include(s => s.LoaiSan)
                .FirstOrDefaultAsync(s => s.Id == request.SanBongId && s.TrangThai);

            if (sanbong == null)
            {
                return Json(new { success = false, message = "Sân bóng không tồn tại hoặc không hoạt động." });
            }

            var cas = await _context.Cas
                .Where(c => request.CaIds.Contains(c.Id) && c.IsActive)
                .ToListAsync();
            if (!cas.Any())
            {
                return Json(new { success = false, message = "Không có ca hợp lệ được chọn." });
            }

            var sanCas = await _context.SanCas
                .Include(sc => sc.NgayTrongTuan)
                .Where(sc => request.SanCaIds.Contains(sc.Id) && sc.SanBongId == request.SanBongId && sc.IsActive)
                .ToListAsync();
            if (sanCas.Count != request.SanCaIds.Count)
            {
                var missingSanCaIds = request.SanCaIds.Where(id => !sanCas.Any(sc => sc.Id == id)).ToList();
                Console.WriteLine($"ConfirmAndBook Error: Missing or inactive SanCaIds: {JsonSerializer.Serialize(missingSanCaIds)}");
                return Json(new { success = false, message = $"Một hoặc nhiều SanCa không hợp lệ hoặc không tồn tại: {string.Join(", ", missingSanCaIds)}." });
            }

            // Kiểm tra SanCaId với ngày tương ứng từ selections
            var selections = request.Selections?.FirstOrDefault()?.Cas ?? new List<CaSelectionDto>();
            foreach (var selection in selections)
            {
                var date = selection.Date;
                var sanCaId = selection.SanCaId;
                var caId = selection.CaId;

                if (!DateTime.TryParseExact(date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out var parsedDate))
                {
                    return Json(new { success = false, message = $"Ngày {date} không hợp lệ." });
                }

                int dayOfWeek = ((int)parsedDate.DayOfWeek + 6) % 7 + 1;
                var sanCa = sanCas.FirstOrDefault(sc => sc.Id == sanCaId);
                if (sanCa == null || !request.CaIds.Contains(sanCa.CaId) || sanCa.CaId != caId)
                {
                    Console.WriteLine($"ConfirmAndBook Error: SanCaId {sanCaId} does not match CaId {caId} or is not in request.CaIds {JsonSerializer.Serialize(request.CaIds)}");
                    return Json(new { success = false, message = $"SanCaId {sanCaId} không khớp với CaId được chọn." });
                }

                if (sanCa.NgayTrongTuan.ThuTu != dayOfWeek)
                {
                    Console.WriteLine($"ConfirmAndBook Error: SanCaId {sanCaId} has NgayTrongTuanId.ThuTu={sanCa.NgayTrongTuan.ThuTu}, expected {dayOfWeek} for date {date}");
                    return Json(new { success = false, message = $"SanCaId {sanCaId} không hợp lệ cho ngày {date} (ngày trong tuần không khớp)." });
                }
            }

            // Kiểm tra ca đã đặt
            foreach (var date in request.Dates)
            {
                if (!DateTime.TryParseExact(date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out var parsedDate))
                {
                    return Json(new { success = false, message = $"Ngày {date} không hợp lệ." });
                }

                var relevantSanCaIds = selections.Where(s => s.Date == date).Select(s => s.SanCaId).ToList();
                var bookedSanCaIds = await _context.HoaDonChiTiets
                    .Where(hdct => hdct.SanBongId == request.SanBongId && hdct.NgayDenSan.Date == parsedDate.Date && hdct.IsActive)
                    .Select(hdct => hdct.SanCaId)
                    .Concat(_context.ThoiGianDatSans
                        .Where(tds => tds.NgayDat.Date == parsedDate.Date && tds.SanCa.SanBongId == request.SanBongId && tds.SanCa.IsActive)
                        .Select(tds => (int?)tds.IdSanCa))
                    .Distinct()
                    .ToListAsync();

                var bookedSanCaIdsForDate = relevantSanCaIds
                    .Where(sanCaId => bookedSanCaIds.Contains(sanCaId))
                    .ToList();
                if (bookedSanCaIdsForDate.Any())
                {
                    var bookedCaNames = sanCas
                        .Where(sc => bookedSanCaIdsForDate.Contains(sc.Id))
                        .Join(cas, sc => sc.CaId, c => c.Id, (sc, c) => c.TenCa)
                        .Distinct()
                        .ToList();
                    Console.WriteLine($"ConfirmAndBook Error: Booked SanCaIds: {JsonSerializer.Serialize(bookedSanCaIdsForDate)} for date {date}");
                    return Json(new
                    {
                        success = false,
                        message = $"Ca {string.Join(", ", bookedCaNames)} đã hết chỗ cho ngày {date}."
                    });
                }
            }

            // Tạo HoaDon và lưu vào cơ sở dữ liệu
            var hoaDon = new HoaDon
            {
                MaHoaDon = GenerateMaHoaDon(),
                KhachHangId = khachHangId,
                NgayLap = DateTime.Now,
                GhiChu = request.GhiChu,
                TrangThaiHoaDon = TrangThaiHoaDon.ChuaThanhToan // Trạng thái ban đầu
            };
            _context.HoaDons.Add(hoaDon);
            await _context.SaveChangesAsync();

            // Lưu chi tiết hóa đơn và dịch vụ đặt bóng
            decimal totalHoaDonCost = 0;
            foreach (var selection in selections)
            {
                if (!DateTime.TryParseExact(selection.Date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out var parsedDate))
                {
                    continue;
                }

                var sanCaId = selection.SanCaId;
                var sanCa = sanCas.FirstOrDefault(sc => sc.Id == sanCaId);
                decimal slotCost = 0;
                decimal nuocUongCost = 0;
                decimal doThueCost = 0;

                if (sanCa != null)
                {
                    var ca = cas.FirstOrDefault(c => c.Id == sanCa.CaId);
                    if (ca != null)
                    {
                        var duration = (ca.EndTime - ca.StartTime).TotalHours;
                        slotCost = (decimal)duration * sanbong.Gia;
                    }
                }

                nuocUongCost = selection.NuocUongSelections?.Sum(nu => nu.GiaBan * nu.Quantity) ?? 0;
                doThueCost = selection.DoThueSelections?.Sum(dt => dt.DonGia * dt.Quantity) ?? 0;
                totalHoaDonCost += slotCost + nuocUongCost + doThueCost;

                var hoaDonChiTiet = new HoaDonChiTiet
                {
                    HoaDonId = hoaDon.Id,
                    SanBongId = request.SanBongId,
                    SanCaId = sanCaId,
                    NgayDenSan = parsedDate,
                    TienThueSan = slotCost,
                    TongTien = slotCost + nuocUongCost + doThueCost,
                    IsActive = true,
                    GhiChu = request.GhiChu,
                    MaChiTietHoaDon = GenerateMaHoaDon(),
                    DichVuDatBongs = new List<DichVuDatBong>()
                };

                // Thêm dịch vụ nước uống vào DichVuDatBong
                if (selection.NuocUongSelections != null && selection.NuocUongSelections.Any())
                {
                    foreach (var nuocUong in selection.NuocUongSelections)
                    {
                        var dichVuDatBong = new DichVuDatBong
                        {
                            NuocUongId = nuocUong.Id,
                            SoLuong = nuocUong.Quantity,
                            TongTien = nuocUong.GiaBan * nuocUong.Quantity,
                            NgayDat = parsedDate,
                            IsActive = true,
                            GhiChu = request.GhiChu,
                            HoaDonChiTietId = hoaDonChiTiet.Id
                        };
                        hoaDonChiTiet.DichVuDatBongs.Add(dichVuDatBong);
                    }
                }

                // Thêm dịch vụ đồ thuê vào DichVuDatBong
                if (selection.DoThueSelections != null && selection.DoThueSelections.Any())
                {
                    foreach (var doThue in selection.DoThueSelections)
                    {
                        var dichVuDatBong = new DichVuDatBong
                        {
                            DoThueId = doThue.Id,
                            SoLuong = doThue.Quantity,
                            TongTien = doThue.DonGia * doThue.Quantity,
                            NgayDat = parsedDate,
                            IsActive = true,
                            GhiChu = request.GhiChu,
                            HoaDonChiTietId = hoaDonChiTiet.Id
                        };
                        hoaDonChiTiet.DichVuDatBongs.Add(dichVuDatBong);
                    }
                }

                _context.HoaDonChiTiets.Add(hoaDonChiTiet);

                var thoiGianDatSan = new ThoiGianDatSan
                {
                    IdSanCa = sanCaId,
                    NgayDat = parsedDate,
                    IdHoaDon = hoaDon.Id
                };
                _context.ThoiGianDatSans.Add(thoiGianDatSan);
            }

            // Gán tổng tiền cho HoaDon
            hoaDon.TongTien = totalHoaDonCost;
            await _context.SaveChangesAsync();

            // Chuyển đổi selections sang CaData để lưu vào BookingData
            var bookingData = new BookingData
            {
                SanBongId = sanbong.Id,
                TenSan = sanbong.TenSan,
                Gia = sanbong.Gia,
                Dates = request.Dates,
                TongTien = totalHoaDonCost,
                Cas = selections.Select(c => new CaData
                {
                    CaId = c.CaId,
                    SanCaId = c.SanCaId,
                    TenCa = c.TenCa,
                    StartTime = c.StartTime,
                    EndTime = c.EndTime,
                    Date = c.Date,
                    NuocUongSelections = c.NuocUongSelections?.Select(n => new NuocUongSelection
                    {
                        Id = n.Id,
                        TenNuocUong = n.TenNuocUong,
                        GiaBan = n.GiaBan,
                        Quantity = n.Quantity
                    }).ToList() ?? new List<NuocUongSelection>(),
                    DoThueSelections = c.DoThueSelections?.Select(d => new DoThueSelection
                    {
                        Id = d.Id,
                        TenDoThue = d.TenDoThue,
                        DonGia = d.DonGia,
                        Quantity = d.Quantity
                    }).ToList() ?? new List<DoThueSelection>()
                }).ToList()
            };

            var orderViewModel = new OrderViewModel
            {
                HoaDonId = hoaDon.Id,
                Booking = bookingData
            };

            HttpContext.Session.SetString("BookingData", JsonSerializer.Serialize(orderViewModel));

            return Json(new
            {
                success = true,
                message = "Đặt sân thành công!",
                redirectUrl = $"/SanBong/Order/{hoaDon.Id}"
            });
        }

        [HttpPost]
        public async Task<IActionResult> BookSlots([FromBody] BookSlotsRequest request)
        {
            // Convert single date to list if provided
            if (!string.IsNullOrEmpty(request.Date) && (request.Dates == null || !request.Dates.Any()))
            {
                request.Dates = new List<string> { request.Date };
            }

            Console.WriteLine($"BookSlots Input: sanBongId={request.SanBongId}, dates={JsonSerializer.Serialize(request.Dates)}, caIds={JsonSerializer.Serialize(request.CaIds)}, sanCaIds={JsonSerializer.Serialize(request.SanCaIds)}");

            // Check for null or empty caIds, sanCaIds, and dates
            if (request.CaIds == null || !request.CaIds.Any() || request.SanCaIds == null || !request.SanCaIds.Any() || request.Dates == null || !request.Dates.Any())
            {
                Console.WriteLine("BookSlots Validation Failed: Empty or null caIds, sanCaIds, or dates");
                return Json(new { success = false, message = "Vui lòng chọn ít nhất một ca, một SanCa và một ngày." });
            }

            // Validate that CaIds and SanCaIds have the same count
            if (request.CaIds.Count != request.SanCaIds.Count)
            {
                Console.WriteLine($"BookSlots Validation Failed: caIds.Count ({request.CaIds.Count}) != sanCaIds.Count ({request.SanCaIds.Count})");
                return Json(new { success = false, message = "Số lượng CaIds và SanCaIds không khớp." });
            }

            var sanbong = await _context.Sanbongs
                .Include(s => s.LoaiSan)
                .FirstOrDefaultAsync(s => s.Id == request.SanBongId && s.TrangThai);

            if (sanbong == null)
            {
                Console.WriteLine($"BookSlots Error: SanBongId={request.SanBongId} not found or inactive");
                return Json(new { success = false, message = "Sân bóng không tồn tại hoặc không hoạt động." });
            }

            var selectedCas = new List<object>();
            var fullyBookedDates = new List<string>();

            foreach (var date in request.Dates)
            {
                if (!DateTime.TryParseExact(date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out var parsedDate))
                {
                    Console.WriteLine($"BookSlots Error: Invalid date format {date}");
                    return Json(new { success = false, message = $"Ngày {date} không hợp lệ." });
                }

                int dayOfWeek = ((int)parsedDate.DayOfWeek + 6) % 7 + 1;
                var availableSanCas = await _context.SanCas
                    .Include(sc => sc.Ca)
                    .Include(sc => sc.NgayTrongTuan)
                    .Where(sc => sc.SanBongId == request.SanBongId
                            && sc.NgayTrongTuan.ThuTu == dayOfWeek
                            && sc.Ca.IsActive
                            && sc.NgayTrongTuan.IsActive
                            && sc.IsActive)
                    .Select(sc => new { sc.CaId, sc.Id, sc.Ca.TenCa, sc.Ca.StartTime, sc.Ca.EndTime })
                    .ToListAsync();

                var bookedSanCaIds = await _context.HoaDonChiTiets
                    .Where(hdct => hdct.SanBongId == request.SanBongId
                            && hdct.NgayDenSan.Date == parsedDate.Date
                            && hdct.IsActive)
                    .Select(hdct => hdct.SanCaId)
                    .Concat(_context.ThoiGianDatSans
                        .Where(tds => tds.NgayDat.Date == parsedDate.Date
                                && tds.SanCa.SanBongId == request.SanBongId
                                && tds.SanCa.IsActive)
                        .Select(tds => (int?)tds.IdSanCa))
                    .Distinct()
                    .ToListAsync();

                var casDb = await _context.Cas
                    .Where(c => request.CaIds.Contains(c.Id) && c.IsActive)
                    .ToListAsync();

                var requestedSanCaIdsForDate = request.SanCaIds
                    .Where(sanCaId => availableSanCas.Any(sc => sc.Id == sanCaId && request.CaIds.Contains(sc.CaId)))
                    .ToList();

                var bookedSanCaIdsForDate = requestedSanCaIdsForDate
                    .Where(sanCaId => bookedSanCaIds.Contains(sanCaId))
                    .ToList();

                if (bookedSanCaIdsForDate.Any())
                {
                    var bookedCaNames = availableSanCas
                        .Where(sc => bookedSanCaIdsForDate.Contains(sc.Id))
                        .Select(sc => sc.TenCa)
                        .Distinct()
                        .ToList();
                    Console.WriteLine($"BookSlots Error: Booked SanCaIds {JsonSerializer.Serialize(bookedSanCaIdsForDate)} for date {date}");
                    return Json(new
                    {
                        success = false,
                        message = $"Ca {string.Join(", ", bookedCaNames)} đã hết chỗ cho ngày {date}."
                    });
                }

                var cas = casDb
                    .Select(c => new
                    {
                        caId = c.Id,
                        sanCaId = availableSanCas.FirstOrDefault(sc => sc.CaId == c.Id && requestedSanCaIdsForDate.Contains(sc.Id))?.Id,
                        tenCa = c.TenCa,
                        startTime = c.StartTime.ToString("HH:mm"),
                        endTime = c.EndTime.ToString("HH:mm"),
                        date = date,
                        isActive = availableSanCas.Any(sc => sc.CaId == c.Id && !bookedSanCaIds.Contains(sc.Id)),
                        nuocUongSelections = new List<NuocUongSelectionDto>(),
                        doThueSelections = new List<DoThueSelectionDto>()
                    })
                    .Where(c => c.isActive && c.sanCaId.HasValue && request.SanCaIds.Contains(c.sanCaId.Value))
                    .ToList();

                Console.WriteLine($"BookSlots Selected Cas for {date}: {JsonSerializer.Serialize(cas)}");

                if (!cas.Any())
                {
                    fullyBookedDates.Add(date);
                }

                selectedCas.AddRange(cas);
            }

            if (!selectedCas.Any())
            {
                Console.WriteLine("BookSlots Error: No valid slots selected");
                return Json(new { success = false, message = "Không có ca nào hợp lệ được chọn." });
            }

            if (fullyBookedDates.Any())
            {
                Console.WriteLine($"BookSlots Error: Fully booked dates: {JsonSerializer.Serialize(fullyBookedDates)}");
                return Json(new
                {
                    success = false,
                    message = $"Các ngày sau không còn ca trống: {string.Join(", ", fullyBookedDates)}."
                });
            }

            var bookingData = new
            {
                sanBongId = sanbong.Id,
                tenSan = sanbong.TenSan,
                gia = sanbong.Gia,
                dates = request.Dates,
                cas = selectedCas
            };

            TempData["BookingData"] = JsonSerializer.Serialize(bookingData);

            Console.WriteLine($"BookSlots Success: {JsonSerializer.Serialize(bookingData)}");
            return Json(new
            {
                success = true,
                message = "Chọn ca thành công!",
                data = bookingData
            });
        }

        [HttpGet]

        public IActionResult Order(int id)

        {

            var tempData = HttpContext.Session.GetString("BookingData");

            Console.WriteLine("Session BookingData: " + tempData);



            if (string.IsNullOrEmpty(tempData))

            {

                Console.WriteLine("TempData[BookingData] is empty");

                return RedirectToAction("Index");

            }



            var model = JsonSerializer.Deserialize<OrderViewModel>(tempData);

            if (model == null)

            {

                Console.WriteLine("Failed to deserialize BookingData");

                return NotFound();

            }



            // Gán HoaDonId từ id

            model.HoaDonId = id;



            // Kiểm tra và khởi tạo Booking nếu null

            if (model.Booking == null)

            {

                Console.WriteLine("Booking is null after deserialization");

                model.Booking = new BookingData

                {

                    Dates = new List<string>(),

                    Cas = new List<CaData>()

                };

            }



            // Kiểm tra dữ liệu Cas

            if (model.Booking.Cas == null)

            {

                Console.WriteLine("Booking.Cas is null");

                model.Booking.Cas = new List<CaData>();

            }



            // Tính toán TongTien

            decimal tongTien = 0;

            foreach (var ca in model.Booking.Cas)

            {

                var duration = (TimeSpan.Parse(ca.EndTime) - TimeSpan.Parse(ca.StartTime)).TotalHours;

                var slotCost = (decimal)duration * model.Booking.Gia;

                var nuocUongCost = ca.NuocUongSelections?.Sum(nu => nu.GiaBan * nu.Quantity) ?? 0;

                var doThueCost = ca.DoThueSelections?.Sum(dt => dt.DonGia * dt.Quantity) ?? 0;

                tongTien += slotCost + nuocUongCost + doThueCost;

            }

            model.Booking.TongTien = tongTien;



            return View(model);

        }

        private string GenerateMaHoaDon()

        {

            // Ví dụ: Sinh mã theo định dạng HD-YYYYMMDD-XXXX

            var datePart = DateTime.Now.ToString("yyyyMMdd");

            var randomPart = new Random().Next(1000, 9999);

            return $"HD-{datePart}-{randomPart}";

        }

        [HttpPost]
        public async Task<IActionResult> ConfirmOrder(OrderViewModel model)
        {
            var sessionData = HttpContext.Session.GetString("BookingData");
            if (string.IsNullOrEmpty(sessionData))
            {
                Console.WriteLine("ConfirmOrder Error: Session BookingData is empty");
                return Json(new { success = false, message = "Dữ liệu đặt sân không tồn tại." });
            }

            var bookingModel = JsonSerializer.Deserialize<OrderViewModel>(sessionData);
            if (bookingModel == null)
            {
                Console.WriteLine("ConfirmOrder Error: Failed to deserialize BookingData");
                return Json(new { success = false, message = "Không thể xử lý dữ liệu đặt sân." });
            }

            // Update customer information
            bookingModel.TenNguoiDat = model.TenNguoiDat;
            bookingModel.SoDienThoaiNguoiDat = model.SoDienThoaiNguoiDat;
            bookingModel.Email = model.Email;
            bookingModel.GhiChu = model.GhiChu;

            // Update HoaDon in database
            var hoaDon = await _context.HoaDons
                .FirstOrDefaultAsync(hd => hd.Id == model.HoaDonId);
            if (hoaDon == null)
            {
                Console.WriteLine($"ConfirmOrder Error: HoaDonId {model.HoaDonId} not found");
                return Json(new { success = false, message = "Hóa đơn không tồn tại." });
            }

            hoaDon.TenNguoiDat = model.TenNguoiDat;
            hoaDon.SoDienThoaiNguoiDat = int.Parse( model.SoDienThoaiNguoiDat);
            hoaDon.Email = model.Email;
            hoaDon.GhiChu = model.GhiChu;
            hoaDon.TrangThaiHoaDon = TrangThaiHoaDon.DaThanhToan;
            hoaDon.TongTienThanhToan = bookingModel.Booking?.TongTien ?? 0;

            try
            {
                await _context.SaveChangesAsync();
                Console.WriteLine($"ConfirmOrder Success: Updated HoaDonId {hoaDon.Id} with customer info");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ConfirmOrder Error: Failed to save HoaDon changes - {ex.Message}");
                return Json(new { success = false, message = "Đã xảy ra lỗi khi lưu thông tin hóa đơn." });
            }

            // Save updated booking data to session
            HttpContext.Session.SetString("BookingData", JsonSerializer.Serialize(bookingModel));

            // Redirect to PaymentController's InitiatePayment action
            return Json(new
            {
                success = true,
                message = "Xác nhận đặt sân thành công! Chuyển đến trang thanh toán.",
                redirectUrl = Url.Action("InitiatePayment", "Payment", new { hoaDonId = hoaDon.Id })
            });
        }
    }



}

