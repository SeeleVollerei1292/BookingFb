namespace bookingfootball.Db_QL
{
    public class HinhThucThanhToan
    {
        public int Id { get; set; } // Mã hình thức thanh toán, có thể là duy nhất
        public string PhuongThucThanhToan { get; set; } // Phương thức thanh toán, có thể là "Tiền mặt", "Chuyển khoản", "Thẻ tín dụng", v.v.
        public string MoTa { get; set; } // Mô tả về hình thức thanh toán
        public bool IsActive { get; set; } = true; // Trạng thái hoạt động của hình thức thanh toán, mặc định là true
        public DateTime NgayTao { get; set; } = DateTime.Now; // Ngày tạo bản ghi, mặc định là ngày hiện tại
        public string GhiChu { get; set; } // Ghi chú về hình thức thanh toán, có thể để trống
    }
}
