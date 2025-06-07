using Duong_API.Data;

namespace bookingfootball.Db_QL
{
    public class HoaDonChiTiet
    {
        public int Id { get; set; } // Mã chi tiết hóa đơn, có thể là duy nhất
        public int HoaDonId { get; set; } // Mã hóa đơn liên kết
        public HoaDon HoaDon { get; set; } // Tham chiếu đến hóa đơn
        public int? SanBongId { get; set; } // Mã sân bóng liên kết
        public Sanbong SanBong { get; set; } // Tham chiếu đến sân bóng
        public int? PhieuGiamGiaId { get; set; } // Mã phiếu giảm giá liên kết, có thể là null nếu không áp dụng
        public PhieuGiamGiaChiTiet PhieuGiamGia { get; set; } // Tham chiếu đến phiếu giảm giá chi tiết
        public int NhanVienId { get; set; } // Mã nhân viên liên kết, có thể là duy nhất
        public NhanVien NhanVien { get; set; } // Tham chiếu đến nhân viên
        public string MaChiTietHoaDon { get; set; } // Mã chi tiết hóa đơn, có thể là duy nhất
        public decimal TongTienDuocGiam { get; set; } // Tổng tiền được giảm, có thể là 0 nếu không áp dụng phiếu giảm giá
        public decimal TongTien { get; set; } // Tổng tiền của chi tiết hóa đơn, có thể là duy nhất
        public decimal TienThueSan { get; set; } // Tiền thuê sân, có thể là duy nhất
        public DateTime NgayDenSan { get; set; } // Ngày đến sân
        public bool IsActive { get; set; } = true; // Trạng thái hoạt động của chi tiết hóa đơn, mặc định là true
        public string GhiChu { get; set; } // Ghi chú về chi tiết hóa đơn, có thể để trống
        public ICollection<DichVuDatBong> DichVuDatBongs { get; set; }
        public ICollection<DoThue> DoThues { get; set; }
    }
}
