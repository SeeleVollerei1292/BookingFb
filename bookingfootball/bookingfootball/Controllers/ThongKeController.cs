using bookingfootball.DTOs;
using bookingfootball.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookingfootball.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ThongKeController : ControllerBase
    {
        private readonly IThongKeRepository _thongKeRepository;

        public ThongKeController(IThongKeRepository thongKeRepository)
        {
            _thongKeRepository = thongKeRepository;
        }

        [HttpGet("doanh-thu-ngay")]
        public async Task<ActionResult<List<DoanhThuTheoNgayDTO>>> GetDoanhThuTheoNgay()
        {
            var result = await _thongKeRepository.GetDoanhThuTheoNgay();
            return Ok(result);
        }

        [HttpGet("doanh-thu-thang")]
        public async Task<ActionResult<List<DoanhThuTheoThangDTO>>> GetDoanhThuTheoThang()
        {
            var result = await _thongKeRepository.GetDoanhThuTheoThang();
            return Ok(result);
        }

        [HttpGet("doanh-thu-nam")]
        public async Task<ActionResult<List<DoanhThuTheoNamDTO>>> GetDoanhThuTheoNam()
        {
            var result = await _thongKeRepository.GetDoanhThuTheoNam();
            return Ok(result);
        }

        [HttpGet("doanh-thu-theo-san")]
        public async Task<ActionResult<List<DoanhThuTheoSanDTO>>> GetDoanhThuTheoSan()
        {
            var result = await _thongKeRepository.GetDoanhThuTheoSan();
            return Ok(result);
        }
        [HttpGet("su-dung-san")]
        public async Task<ActionResult<ThongKeSuDungSanDTO>> GetThongKeSuDungSan()
        {
            var result = await _thongKeRepository.GetThongKeSuDungSan();
            return Ok(result);
        }

        [HttpPost("loc-thong-ke")]
        public async Task<ActionResult<ThongKeDTO>> GetFilteredStatistics([FromBody] FilterStatisticsRequest request)
        {
            var result = await _thongKeRepository.GetFilteredStatistics(request.FromDate, request.ToDate);
            return Ok(result);
        }
    }

    public class FilterStatisticsRequest
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}

