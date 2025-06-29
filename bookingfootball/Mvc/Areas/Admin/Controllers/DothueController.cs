using Duong_API.Data;
using DuongPia.Areas.Admin.IServices;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DuongPia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DothueController : Controller
    {
        private readonly IDoThueService _doThueService;
        public DothueController(IDoThueService doThueService)
        {
            _doThueService = doThueService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string searchString, string trangThai, int? giaMin, int? giaMax)
        {

            if (!string.IsNullOrEmpty(searchString))
            {
                var nuocUongs = await _doThueService.GetAllAsync();
                var filteredNuocUongs = nuocUongs.Where(n => n.TenDoThue.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                return View(filteredNuocUongs);
            }
            var list = await _doThueService.GetAllAsync();
            if (!string.IsNullOrEmpty(trangThai))
            {
                bool status = bool.Parse(trangThai);
                list = list.Where(x => x.TrangThai == status).ToList();
            }
            if (giaMin.HasValue)
            {
                list = list.Where(x => x.DonGia >= giaMin.Value).ToList();
            }

            if (giaMax.HasValue)
            {
                list = list.Where(x => x.DonGia <= giaMax.Value).ToList();
            }
            ViewBag.SearchString = searchString;
            ViewBag.TrangThai = trangThai;
            ViewBag.GiaMin = giaMin;
            ViewBag.GiaMax = giaMax;
            return View(list);

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DoThue doThue , IFormFile HinhAnh)
        {
            if (HinhAnh != null && HinhAnh.Length > 0)
            {
                var fileName = Path.GetFileName(HinhAnh.FileName);
                var filePath = Path.Combine("wwwroot/images/", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await HinhAnh.CopyToAsync(stream);
                }
                doThue.HinhAnh = "/images/" + fileName;
            }
            await _doThueService.CreateAsync(doThue);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var doThue = await _doThueService.GetByIdAsync(id);
            ViewBag.EditView = doThue;
            var list = await _doThueService.GetAllAsync();
            return View("Index",list);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(DoThue doThue,IFormFile HinhAnh)
        {
            if (HinhAnh != null && HinhAnh.Length > 0)
            {
                var fileName = Path.GetFileName(HinhAnh.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await HinhAnh.CopyToAsync(stream);
                }
                doThue.HinhAnh = "/images/" + fileName;
            }
            await _doThueService.UpdateAsyncs(doThue.Id, doThue);
            return RedirectToAction("Index", "Dothue", new { area = "Admin" });
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var doThue = await _doThueService.GetByIdAsync(id);
            if (doThue == null)
            {
                return NotFound();
            }
            return View(doThue);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _doThueService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
