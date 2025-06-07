using bookingfootball.Data;
using bookingfootball.Db_QL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoaiSanController : Controller
    {
        private readonly SbDbcontext _context;

        public LoaiSanController(SbDbcontext context)
        {
            _context = context;
        }

        // Hiển thị danh sách loại sân
        public async Task<IActionResult> Index()
        {
            var loaiSans = await _context.LoaiSans.ToListAsync();
            return View(loaiSans);
        }

        // Hiển thị form thêm mới
        public IActionResult Create()
        {
            return View("Index");
        }

        // Xử lý thêm mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LoaiSan loaiSan)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.LoaiSans.Add(loaiSan);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Thêm loại sân thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Lỗi khi thêm loại sân: {ex.Message}";
                    return RedirectToAction(nameof(Index));
                }
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            TempData["ErrorMessage"] = "Dữ liệu không hợp lệ: " + string.Join("; ", errors);
            Console.WriteLine("ModelState Errors: " + string.Join("; ", errors));

            ViewBag.EditModel = loaiSan;
            var loaiSans = await _context.LoaiSans.ToListAsync();
            return View("Index", loaiSans);
        }

        // Hiển thị form chỉnh sửa
        public async Task<IActionResult> Edit(int id)
        {
            var loaiSan = await _context.LoaiSans.FindAsync(id);
            if (loaiSan == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy loại sân.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.EditModel = loaiSan;
            var loaiSans = await _context.LoaiSans.ToListAsync();
            return View("Index", loaiSans);
        }

        // Xử lý chỉnh sửa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LoaiSan loaiSan)
        {
            if (id != loaiSan.Id)
            {
                TempData["ErrorMessage"] = "ID loại sân không khớp.";
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiSan);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Cập nhật loại sân thành công!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.LoaiSans.Any(e => e.Id == id))
                    {
                        TempData["ErrorMessage"] = "Không tìm thấy loại sân.";
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

            ViewBag.EditModel = loaiSan;
            var loaiSans = await _context.LoaiSans.ToListAsync();
            return View("Index", loaiSans);
        }

        // Cập nhật trạng thái (ẩn/hiện loại sân)
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var loaiSan = await _context.LoaiSans.FindAsync(id);
            if (loaiSan == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy loại sân.";
                return RedirectToAction(nameof(Index));
            }

            loaiSan.TrangThai = !loaiSan.TrangThai;
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Cập nhật trạng thái thành công!";
            return RedirectToAction(nameof(Index));
        }
    }
}