namespace bookingfootball.Db_QL
{
    public class CT_HinhThucThanhToan
    {
        public int Id { get; set; } // Mã hình thức thanh toán, có thể là duy nhất
        public int HoaDonChiTietId { get; set; } // Mã hóa đơn chi tiết, có thể là duy nhất
        public HoaDonChiTiet HoaDonChiTiet { get; set; } // Tham chiếu đến hóa đơn chi tiết, có thể là null nếu không có liên kết
        public int HinhThucThanhToanId { get; set; } // Mã hình thức thanh toán, liên kết với bảng HinhThucThanhToan
        public HinhThucThanhToan HinhThucThanhToan { get; set; } // Tham chiếu đến hình thức thanh toán, có thể là null nếu không có liên kết
        public string TenHinhThucThanhToan { get; set; } // Tên hình thức thanh toán, có thể là duy nhất
        public decimal TongTien { get; set; } // Số tiền thanh toán, có thể là số thập phân
        public string MoTa { get; set; } // Mô tả hình thức thanh toán, có thể là một chuỗi mô tả
        public bool IsActive { get; set; } = true; // Trạng thái hoạt động của hình thức thanh toán, mặc định là true
    }
}
