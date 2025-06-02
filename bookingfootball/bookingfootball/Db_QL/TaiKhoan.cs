namespace bookingfootball.Db_QL
{
    public class TaiKhoan
    {
        public int Id { get; set; } // Mã tài khoản, có thể là duy nhất
        public string TenDangNhap { get; set; } // Tên đăng nhập, có thể là duy nhất
        public string MatKhau { get; set; } // Mật khẩu đã được mã hóa
        public string Email { get; set; } // Email, có thể là duy nhất
        public int NhanVienId { get; set; } // Mã nhân viên liên kết với tài khoản, có thể là null nếu không liên kết
        public NhanVien NhanVien { get; set; } // Thông tin nhân viên liên kết, có thể là null nếu không liên kết
        public int KhachHangId { get; set; } // Mã khách hàng liên kết với tài khoản, có thể là null nếu không liên kết
        public KhachHang KhachHang { get; set; } // Thông tin khách hàng liên kết, có thể là null nếu không liên kết
        public string SoDienThoai { get; set; } // Số điện thoại, có thể là duy nhất
        public bool IsActive { get; set; } = true; // Trạng thái hoạt động của tài khoản
        public DateTime NgayTao { get; set; } = DateTime.Now; // Ngày tạo tài khoản
        public DateTime? NgayCapNhatCuoi { get; set; } // Ngày cập nhật cuối cùng (nếu có)
    }
}
