using bookingfootball.Db_QL;
using Microsoft.AspNetCore.Mvc;
using Mvc.Areas.Admin.IServices;

namespace Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CaController : Controller
    {
        private readonly ICaServices _caServices;
        public CaController(ICaServices caServices)
        {
            _caServices = caServices;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cas = await _caServices.GetCasAsync();
            return View(cas);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var ca = await _caServices.GetCaByIdAsync(id);
            if (ca == null)
            {
                return NotFound();
            }
            return View(ca);
        }
		[HttpPost]
		public async Task<IActionResult> Create(Ca ca)
		{
			await _caServices.CreateCaAsync(ca);
			return RedirectToAction("Index", "Ca", new { area = "Admin" });
		}
        [HttpGet]
		public async Task<IActionResult> Edit(int id)
        {
            var ca = await _caServices.GetCaByIdAsync(id);
            ViewBag.EditModel = ca;
            var cas = await _caServices.GetCasAsync();
            return View("Index", cas);
        }
        [HttpPost]
        public async Task<IActionResult> Edit( Ca ca)
        {
			await _caServices.UpdateCaAsync(ca.Id, ca);
			return RedirectToAction("Index", "Ca", new { area = "Admin" });
		}
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _caServices.DeleteCaAsync(id);
                return RedirectToAction("Index", "Ca", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Index", await _caServices.GetCasAsync());
            }
        }
    }
}
