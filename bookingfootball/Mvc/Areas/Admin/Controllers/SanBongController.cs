using bookingfootball.Data;
using bookingfootball.Db_QL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SanbongController : Controller
    {
        private readonly SbDbcontext _context;

        public SanbongController(SbDbcontext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var sanbongs = await _context.Sanbongs
                .Include(s => s.LoaiSan)
                .ToListAsync();

            ViewBag.LoaiSanList = await _context.LoaiSans.Where(l => l.TrangThai).ToListAsync();
            return View(sanbongs);
        }

        public IActionResult Create()
        {
            ViewBag.LoaiSanId = new SelectList(_context.LoaiSans.Where(l => l.TrangThai), "Id", "TenLoaiSan");
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sanbong sanbong)
        {
            if (sanbong.LoaiSanId.HasValue && !await _context.LoaiSans.AnyAsync(l => l.Id == sanbong.LoaiSanId && l.TrangThai))
            {
                ModelState.AddModelError("LoaiSanId", "Loại sân không hợp lệ hoặc không hoạt động.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Sanbongs.Add(sanbong);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Thêm sân bóng thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Lỗi khi thêm sân bóng: {ex.Message}";
                    return RedirectToAction(nameof(Index));
                }
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            TempData["ErrorMessage"] = "Dữ liệu không hợp lệ: " + string.Join("; ", errors);
            Console.WriteLine("ModelState Errors: " + string.Join("; ", errors));

            var sanbongs = await _context.Sanbongs.Include(s => s.LoaiSan).ToListAsync();
            ViewBag.LoaiSanList = await _context.LoaiSans.Where(l => l.TrangThai).ToListAsync();
            ViewBag.EditModel = sanbong;
            return View("Index", sanbongs);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var sanbong = await _context.Sanbongs.FindAsync(id);
            if (sanbong == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy sân bóng.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.LoaiSanId = new SelectList(_context.LoaiSans.Where(l => l.TrangThai), "Id", "TenLoaiSan", sanbong.LoaiSanId);
            ViewBag.EditModel = sanbong;
            var sanbongs = await _context.Sanbongs.Include(s => s.LoaiSan).ToListAsync();
            ViewBag.LoaiSanList = await _context.LoaiSans.Where(l => l.TrangThai).ToListAsync();
            return View("Index", sanbongs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Sanbong sanbong)
        {
            if (id != sanbong.Id)
            {
                TempData["ErrorMessage"] = "ID sân bóng không khớp.";
                return RedirectToAction(nameof(Index));
            }

            if (sanbong.LoaiSanId.HasValue && !await _context.LoaiSans.AnyAsync(l => l.Id == sanbong.LoaiSanId && l.TrangThai))
            {
                ModelState.AddModelError("LoaiSanId", "Loại sân không hợp lệ hoặc không hoạt động.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sanbong);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Cập nhật sân bóng thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Sanbongs.Any(e => e.Id == id))
                    {
                        TempData["ErrorMessage"] = "Không tìm thấy sân bóng.";
                        return RedirectToAction(nameof(Index));
                    }
                    throw;
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Lỗi khi lưu dữ liệu: {ex.Message}";
                    return RedirectToAction(nameof(Index));
                }
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            TempData["ErrorMessage"] = "Dữ liệu không hợp lệ: " + string.Join("; ", errors);
            Console.WriteLine("ModelState Errors: " + string.Join("; ", errors));

            var sanbongs = await _context.Sanbongs.Include(s => s.LoaiSan).ToListAsync();
            ViewBag.LoaiSanList = await _context.LoaiSans.Where(l => l.TrangThai).ToListAsync();
            ViewBag.EditModel = sanbong;
            return View("Index", sanbongs);
        }

        public async Task<IActionResult> ToggleStatus(int id)
        {
            var sanbong = await _context.Sanbongs.FindAsync(id);
            if (sanbong == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy sân bóng.";
                return RedirectToAction(nameof(Index));
            }

            sanbong.TrangThai = !sanbong.TrangThai;
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Cập nhật trạng thái thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}