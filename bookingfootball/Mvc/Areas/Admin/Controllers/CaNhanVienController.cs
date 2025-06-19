using bookingfootball.DTOs;
using Microsoft.AspNetCore.Mvc;
using Mvc.Areas.Admin.IServices;

namespace Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CaNhanVienController : Controller
    {
        private readonly ICaNhanVienService _service;

        public CaNhanVienController(ICaNhanVienService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            var list = await _service.GetAllAsync();
            CaNhanVienDTO formModel = id.HasValue ? (await _service.GetByIdAsync(id.Value) ?? new CaNhanVienDTO()) : new CaNhanVienDTO();
            return View(new Tuple<IEnumerable<CaNhanVienDTO>, CaNhanVienDTO>(list, formModel));
        }

    
        [HttpPost]
        public async Task<IActionResult> Create(CaNhanVienDTO dto)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(dto);
                return RedirectToAction("Index", "CaNhanVien", new {area = "Admin"});
            }
            var list = await _service.GetAllAsync();
            return View("Index", new Tuple<IEnumerable<CaNhanVienDTO>, CaNhanVienDTO>(list, dto));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CaNhanVienDTO dto)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(dto, id);
                return RedirectToAction("Index", "CaNhanVien", new { area = "Admin" });
            }
            var list = await _service.GetAllAsync();
            return View("Index", new Tuple<IEnumerable<CaNhanVienDTO>, CaNhanVienDTO>(list, dto));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var nv = await _service.GetByIdAsync(id);
            if(nv == null) 
            {
                return NotFound();
            }
            return View(nv);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id) 
        {
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        
    }
}