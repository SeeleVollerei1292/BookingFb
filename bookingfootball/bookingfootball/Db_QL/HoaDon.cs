namespace bookingfootball.Db_QL
{
    public class HoaDon
    {
        public int Id { get; set; } // Mã hóa đơn, có thể là duy nhất
        public int NhanVienId { get; set; } // Mã nhân viên, liên kết với bảng NhanVien	
        public NhanVien NhanVien { get; set; } // Thông tin nhân viên liên kết
        public int KhachHangId { get; set; } // Mã khách hàng, liên kết với bảng KhachHang
        public KhachHang KhachHang { get; set; } // Thông tin khách hàng liên kết
        public string MaHoaDon { get; set; } // Mã hóa đơn, có thể là duy nhất
        public DateTime NgayLap { get; set; } // Ngày lập hóa đơn
        public decimal TongTien { get; set; } // Tổng tiền của hóa đơn
        public decimal TienCoc { get; set; } // Tiền cọc của hóa đơn

        public decimal TongTienThanhToan { get; set; } // Tổng tiền thanh toán của hóa đơn
        public string GhiChu { get; set; } // Ghi chú về hóa đơn, có thể để trống
        public ICollection<LichSuHoaDon> LichSuHoaDons { get; set; }
        public ICollection<HoaDonChiTiet> HoaDonChiTiets { get; set; }
    }
}
