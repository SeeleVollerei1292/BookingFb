using Mvc.Areas.Admin.Model;
using Microsoft.AspNetCore.Mvc;
using Mvc.Areas.Admin.IServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NuocuongController : Controller
    {
        private readonly INuocuongServices _nuocuongServices;
        public NuocuongController(INuocuongServices nuocuongServices)
        {
            _nuocuongServices = nuocuongServices;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                var nuocUongs = await _nuocuongServices.GetAllNuocUongAsync();
                var filteredNuocUongs = nuocUongs.Where(n => n.TenNuocUong.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                return View(filteredNuocUongs);
            }
            var list = await _nuocuongServices.GetAllNuocUongAsync();
            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var nuocUong = await _nuocuongServices.GetNuocUongById(id);
            if (nuocUong == null)
            {
                return NotFound();
            }
            return View(nuocUong);
        }
        [HttpPost]
        public async Task<IActionResult> Create(NuocUong nuocUong, IFormFile Anh)
        {
            if (Anh != null && Anh.Length > 0)
            {
                var fileName = Path.GetFileName(Anh.FileName);
                var filePath = Path.Combine("wwwroot/images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Anh.CopyToAsync(stream);
                }
                nuocUong.Anh = "/images/" + fileName;
            }

            await _nuocuongServices.CreateNuocUong(nuocUong);
            return RedirectToAction("Index", "Nuocuong", new { area = "Admin" });
        }
     
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var nuocUong = await _nuocuongServices.GetNuocUongById(id);
            ViewBag.EditModel = nuocUong;
            var list = await _nuocuongServices.GetAllNuocUongAsync();
            return View("Index", list);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NuocUong model, IFormFile Anh)
        {
            if (Anh != null && Anh.Length > 0)
            {
                var fileName = Path.GetFileName(Anh.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await Anh.CopyToAsync(stream);
                }
                model.Anh = "/images/" + fileName;
            }

            await _nuocuongServices.UpdateNuocUong(model.Id,model);
            return RedirectToAction("Index", "Nuocuong", new { area = "Admin" });
        }
       
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var nuocUong = await _nuocuongServices.GetNuocUongById(id);
            if (nuocUong == null)
            {
                return NotFound();
            }
            return View(nuocUong);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _nuocuongServices.DisableNuocUong(id);
            return RedirectToAction("Index", "Nuocuong", new { area = "Admin" });
        }
        
    }
}
