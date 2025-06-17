namespace bookingfootball.Db_QL
{
    public class DichVuDatBong
    {
        public int Id { get; set; } // Mã dịch vụ đặt bóng, có thể là duy nhất
        public int NuocUongId { get; set; } // Mã nước uống, liên kết với bảng NuocUong
        public NuocUong NuocUong { get; set; } // Thông tin nước uống liên kết
        public int? ThueSanId { get; set; } // Mã thuế sân, liên kết với bảng ThueSan
        public Thue ? Thues { get; set; } // Thông tin thuê sân liên kết
        public int SoLuong { get; set; } // Số lượng dịch vụ đặt bóng, có thể là số nguyên dương\
        public decimal TongTien { get; set; } // Tổng tiền của dịch vụ đặt bóng, có thể là số thập phân
        public DateTime NgayDat { get; set; } // Ngày đặt dịch vụ, có thể là ngày và giờ cụ thể
        public bool IsActive { get; set; } = true; // Trạng thái hoạt động của dịch vụ đặt bóng, mặc định là true
        public string GhiChu { get; set; } // Ghi chú về dịch vụ đặt bóng, nếu cần
        public int HoaDonChiTietId { get; set; }
        public HoaDonChiTiet HoaDonChiTiet { get; set; }
    }
}
