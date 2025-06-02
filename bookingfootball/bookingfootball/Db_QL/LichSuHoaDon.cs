namespace bookingfootball.Db_QL
{
    public class LichSuHoaDon
    {
        public int Id { get; set; } // Mã lịch sử hóa đơn, có thể là duy nhất
        public int HoaDonId { get; set; } // Mã hóa đơn, liên kết với bảng HoaDon
        public HoaDon HoaDon { get; set; } // Thông tin hóa đơn liên kết
        public DateTime NgayThucHien { get; set; } // Ngày thực hiện lịch sử hóa đơn
        public string NguoiTao { get; set; } // Người tạo lịch sử hóa đơn, có thể là tên hoặc mã nhân viên
        public string NgươiCapNhat { get; set; } // Người cập nhật lịch sử hóa đơn, có thể là tên hoặc mã nhân viên
        public DateTime NgayCapNhat { get; set; } // Ngày cập nhật lịch sử hóa đơn
        public string GhiChu { get; set; } // Ghi chú về lịch sử hóa đơn, có thể để trống
        public bool IsActive { get; set; } = true; // Trạng thái hoạt động của lịch sử hóa đơn, mặc định là true
    }
}
