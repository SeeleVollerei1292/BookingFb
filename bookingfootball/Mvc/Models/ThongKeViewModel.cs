using bookingfootball.DTOs;

namespace Mvc.Models
{
    public class ThongKeViewModel
    {
        public List<DoanhThuTheoNgayDTO> DoanhThuNgay { get; set; } = new List<DoanhThuTheoNgayDTO>();
        public List<DoanhThuTheoThangDTO> DoanhThuThang { get; set; } = new List<DoanhThuTheoThangDTO>();
        public List<DoanhThuTheoNamDTO> DoanhThuNam { get; set; } = new List<DoanhThuTheoNamDTO>();
        public List<DoanhThuTheoSanDTO> DoanhThuTheoSan { get; set; } = new List<DoanhThuTheoSanDTO>();
        public ThongKeSuDungSanDTO ThongKeSuDungSan { get; set; } = new ThongKeSuDungSanDTO();
    }
}
