namespace bookingfootball.Db_QL
{
    public class KhachHang
    {
        public int Id { get; set; } // Mã khách hàng, có thể là duy nhất
        public string HoTen { get; set; } // Họ và tên của khách hàng
        public string Password { get; set; } // Mật khẩu của khách hàng, nên được mã hóa
        public string GioiTinh { get; set; } // Giới tính của khách hàng, có thể là "Nam", "Nữ" hoặc "Khác"
        public string SoDienThoai { get; set; } // Số điện thoại của khách hàng, có thể là duy nhất
        public string Email { get; set; } // Email của khách hàng, có thể là duy nhất
        public DateTime CreateStamp { get; set; } = DateTime.UtcNow; // Ngày tạo tài khoản, mặc định là thời điểm hiện tại
        public bool IsActive { get; set; } = true; // Trạng thái hoạt động của khách hàng, mặc định là true
        public ICollection<HoaDon> HoaDons { get; set; }
        public TaiKhoan TaiKhoan { get; set; } // Tài khoản liên kết với khách hàng, có thể là null nếu không liên kết
        public ICollection<DiaChiKhachHang> DiaChiKhachHangs { get; set; }
        public ICollection<PhieuGiamGiaChiTiet> PhieuGiamGiaChiTiets { get; set; }
    }
}
