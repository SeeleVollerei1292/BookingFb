using bookingfootball.DTOs;
using bookingfootball.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc.Areas.Admin.IService;


namespace Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles ="Admin")]
    public class ThongKeController : Controller
    {
            private readonly IThongKeService _thongKeService;

            public ThongKeController(IThongKeService thongKeService)
            {
                _thongKeService = thongKeService;
            }

            public async Task<IActionResult> Index()
            {
                try
                {
                    var model = await _thongKeService.GetThongKeAsync();
                    return View(model);
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Đã xảy ra lỗi khi tải dữ liệu thống kê: " + ex.Message;
                    return View();
                }
            }

            [HttpPost]
            public async Task<IActionResult> FilterStatistics(DateTime? fromDate, DateTime? toDate)
            {
                try
                {
                    var filteredStats = await _thongKeService.FilterStatisticsAsync(fromDate, toDate);
                    return PartialView("_FilteredStatistics", filteredStats);
                }
                catch (Exception ex)
                {
                    return PartialView("_Error", new { Message = "Đã xảy ra lỗi: " + ex.Message });
                }
            }

            public IActionResult DoanhThuNgay()
            {
                return PartialView("_DoanhThuNgay");
            }

            public IActionResult DoanhThuThang()
            {
                return PartialView("_DoanhThuThang");
            }

            public IActionResult DoanhThuNam()
            {
                return PartialView("_DoanhThuNam");
            }

            public IActionResult DoanhThuTheoSan()
            {
                return PartialView("_DoanhThuTheoSan");
            }

            public IActionResult ThongKeKhachHang()
            {
                return PartialView("_ThongKeKhachHang");
            }

            public IActionResult ThongKeSuDungSan()
            {
                return PartialView("_ThongKeSuDungSan");
            }
        }
}

