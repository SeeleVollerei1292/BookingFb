using Microsoft.AspNetCore.Mvc;
using bookingfootball.Persistence;
using Microsoft.EntityFrameworkCore;
using Mvc.Models;
using bookingfootball.Data;

namespace Mvc.Controllers
{
    public class LichSuController : Controller
    {
        private readonly SbDbcontext _context;

        public LichSuController(SbDbcontext context)
        {
            _context = context;
        }

        public IActionResult LichSuDatSan()
        {
            var khachHangIdClaim = User.FindFirst("KhachHangId");
            if (khachHangIdClaim == null)
            {
                return RedirectToAction("SignIn", "Auth");
            }

            int khachHangId = int.Parse(khachHangIdClaim.Value);

            var lichSu = _context.HoaDonChiTiets
                .Include(h => h.HoaDon)
                .Include(h => h.SanBong)
                .Where(h => h.HoaDon.KhachHangId == khachHangId)
                .Select(h => new LichSuDatSanViewModel
                {
                    TenSan = h.SanBong.TenSan,
                    Gia = h.SanBong.Gia,
                    MoTa = h.SanBong.MoTa
                })
                .ToList();

            return View(lichSu);
        }

    }
}
