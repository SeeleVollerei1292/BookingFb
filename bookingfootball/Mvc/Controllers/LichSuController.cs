using Microsoft.AspNetCore.Mvc;
using bookingfootball.Persistence;

using Mvc.Models;
using bookingfootball.Data;
using Microsoft.EntityFrameworkCore;

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
                .Include(h => h.DichVuDatBongs).ThenInclude(d => d.NuocUong)
                .Include(h => h.DichVuDatBongs).ThenInclude(d => d.Dothue)
                .Include(h => h.DichVuDatBongs).ThenInclude(d => d.Thues)
                .Where(h => h.HoaDon.KhachHangId == khachHangId)
                .Select(h => new LichSuDatSanViewModel
                {
                    TenSan = h.SanBong.TenSan,
                    Gia = h.SanBong.Gia,
                    MoTa = h.SanBong.MoTa,
                    NgayDenSan = h.NgayDenSan,
                    TongTien = h.TongTien,
                    DichVuDatBongList = h.DichVuDatBongs.Select(d => new DichVuViewModel
                    {
                        TenNuocUong = d.NuocUong != null ? d.NuocUong.TenNuocUong : null,
                        TenDoThue = d.Dothue != null ? d.Dothue.TenDoThue : null,
                        TenThueSan = d.Thues != null ? d.Thues.TenThue : null,
                        SoLuong = d.SoLuong ,
                        SoLuongDoThue = d.SoLuongDoThue ?? 0,
                        TongTien = d.TongTien,
                        GhiChu = d.GhiChu
                    }).ToList()
                })
                .ToList();

            return View(lichSu); // ✅ phải là List<LichSuDatSanViewModel>
        }


    }
}
