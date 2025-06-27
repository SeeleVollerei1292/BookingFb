using bookingfootball.Db_QL;
using Microsoft.AspNetCore.Mvc;
using Mvc.Areas.Admin.IServices;

namespace Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SanCaController : Controller
    {
        private readonly ISancaService _sancaService;

        public SanCaController(ISancaService sancaService)
        {
            _sancaService = sancaService;
        }
		private async Task LoadDropdowns()
		{
            ViewBag.Cas = await _sancaService.GetCasAsync();
            ViewBag.SanBongs = await _sancaService.GetSanBongsAsync();
            ViewBag.ThuTuans = await _sancaService.GetThuTuansAsync();
        }

		public async Task<IActionResult> Index()
		{
			await LoadDropdowns(); 
			var sanCas = await _sancaService.GetSanCasAsync();
			return View(sanCas);
		}

		public async Task<IActionResult> Details(int id)
        {
            var sanCa = await _sancaService.GetSanCaByIdAsync(id);
            if (sanCa == null)
            {
                return NotFound();
            }
            return View(sanCa);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SanCa sanCa)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdowns();
                ViewBag.EditModel = null; // Tránh hiểu nhầm Edit mode
                var sanCas = await _sancaService.GetSanCasAsync();
                return View("Index", sanCas);
            }

            try
            {
                await _sancaService.CreateSanCaAsync(sanCa);
                return RedirectToAction("Index", "SanCa", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                await LoadDropdowns(); // Cần reload dropdown
                ViewBag.Error = ex.Message;
                ViewBag.EditModel = null;
                var sanCas = await _sancaService.GetSanCasAsync();
                return View("Index", sanCas);
            }
        }

		public async Task<IActionResult> Edit(int id)
		{
			await LoadDropdowns();
			var sanCa = await _sancaService.GetSanCaByIdAsync(id);
			ViewBag.EditModel = sanCa;
			var sanCas = await _sancaService.GetSanCasAsync();
			return View("Index", sanCas);
		}

		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SanCa sanCa)
        {
            try
            {
                await _sancaService.UpdateSanCaAsync(sanCa.Id, sanCa);
                return RedirectToAction("Index", "SanCa", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("Edit", new { id = sanCa.Id });
            }
        }

        //public async Task<IActionResult> Delete(int id)
        //{
        //    var sanCa = await _sancaService.GetSanCaByIdAsync(id);
        //    if (sanCa == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(sanCa);
        //}

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _sancaService.DeleteSanCaAsync(id);
            return RedirectToAction("Index", "SanCa", new { area = "Admin" });
        }
    }
}