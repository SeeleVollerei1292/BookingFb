using Mvc.Areas.Admin.Model;
using Microsoft.AspNetCore.Mvc;
using Mvc.Areas.Admin.IServices;

namespace Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NhanvienController : Controller
    {
        private readonly INhanVienServices _nhanVienServices;
        public NhanvienController(INhanVienServices nhanVienServices)
        {
            _nhanVienServices = nhanVienServices;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                var nhanViens = await _nhanVienServices.GetAllNhanVienAsync();
                var filteredNhanViens = nhanViens.Where(nv => nv.FullName.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                return View(filteredNhanViens);
            }
            var nhanVienss = await _nhanVienServices.GetAllNhanVienAsync();
            return View(nhanVienss);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var nhanVien = await _nhanVienServices.GetNhanVienById(id);
            if (nhanVien == null)
            {
                return NotFound();
            }
            return View(nhanVien);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NhanVien nv)
        {
            if (ModelState.IsValid)
            {
                await _nhanVienServices.CreateNhanvien(nv);
                return RedirectToAction("Index", "NhanVien", new { area = "Admin" });
            }
            return View(nv);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var nhanVien = await _nhanVienServices.GetNhanVienById(id);
            ViewBag.EditModel = nhanVien;
            var listNhanVien = await _nhanVienServices.GetAllNhanVienAsync();
            return View("Index",listNhanVien);
        }
        [HttpPost]

        public async Task<IActionResult> Edit( NhanVien nv)
        {
            await _nhanVienServices.UpdateNhanVien(nv.Id, nv);
            return RedirectToAction("Index","NhanVien" , new {area = "Admin"});
        }
        [HttpPost]
        public async Task<IActionResult> Disable(int id)
        {
            try
            {
                await _nhanVienServices.DisableNhanVien(id);
                return RedirectToAction("Index","NhanVien", new {area = "Admin"});
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error deleting NhanVien: {ex.Message}");
                return View();
            }
        }
    } 
}
