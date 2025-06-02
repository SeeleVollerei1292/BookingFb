namespace bookingfootball.Db_QL
{
    public class PhieuGiamGiaChiTiet
    {
        public int Id { get; set; } // Mã chi tiết phiếu giảm giá, có thể là duy nhất
        public int PhieuGiamGiaId { get; set; } // Mã phiếu giảm giá, liên kết với bảng PhieuGiamGia
        public PhieuGiamGia PhieuGiamGia { get; set; } // Thông tin phiếu giảm giá liên kết
        public int KhachHangId { get; set; } // Mã khách hàng, liên kết với bảng KhachHang	
        public KhachHang KhachHang { get; set; } // Thông tin khách hàng liên kết
        public decimal SoTienGiam { get; set; } // Số tiền giảm, có thể là duy nhất
        public DateTime NgayTao { get; set; } // Ngày tạo phiếu giảm giá
        public DateTime NgayHetHan { get; set; } // Ngày hết hạn của phiếu giảm giá
        public bool IsActive { get; set; } = true; // Trạng thái hoạt động của chi tiết phiếu giảm giá, mặc định là true
        public ICollection<HoaDonChiTiet> HoaDonChiTiets { get; set; }
    }
}
