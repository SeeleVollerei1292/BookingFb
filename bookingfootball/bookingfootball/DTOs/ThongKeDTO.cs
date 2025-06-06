namespace bookingfootball.DTOs
{
    public class ThongKeDTO
    {
        public decimal? TongDoanhThu { get; set; }
        public List<DoanhThuTheoThoiGianDTO> DoanhThuTheoThoiGian { get; set; }
        public List<DoanhThuTheoSanDTO> DoanhThuTheoSan { get; set; }
        public int SoLuongKhachHangMoi { get; set; }
        public int SoLuongKhachHangThuongXuyen { get; set; }
        public List<SuDungSanDTO> SuDungSan { get; set; }
        public List<ThoiGianCaoDiemDTO> ThoiGianCaoDiem { get; set; }
        public List<DoanhThuTheoNgayDTO> DoanhThuTheoNgay { get; set; }
        public List<DoanhThuTheoThangDTO> DoanhThuTheoThang { get; set; }
        public List<DoanhThuTheoNamDTO> DoanhThuTheoNam { get; set; }
    }
}
