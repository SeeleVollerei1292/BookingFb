namespace bookingfootball.Db_QL
{
    public class NhanVien
    {
        public int Id { get; set; }
        public string FullName { get; set; } // Tên đầy đủ của nhân viên, có thể là duy nhất
        public string StaffCode { get; set; } // Mã nhân viên, có thể là duy nhất
        public string Username { get; set; } // Tên đăng nhập của nhân viên, có thể là duy nhất	
        public string Password { get; set; } // Mật khẩu, có thể mã hóa
        public string Email { get; set; } // Email của nhân viên
        public string PhoneNumber { get; set; } // Số điện thoại của nhân viên
        public string Address { get; set; } // Địa chỉ của nhân viên
        public bool IsActive { get; set; } = true; // Trạng thái hoạt động của nhân viên, mặc định là true
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Ngày tạo tài khoản, mặc định là thời điểm hiện tại
        public DateTime? UpdatedAt { get; set; } // Ngày cập nhật tài khoản, có thể null nếu chưa cập nhật
        public string IdentityNumber { get; set; } // Số CMND/CCCD hoặc mã định danh khác của nhân viên
        public ICollection<LichLamViec>? LichLamViecs { get; set; }
        public ICollection<DiemDanh>? DiemDanhs { get; set; }
        public TaiKhoan? TaiKhoan { get; set; }
        public ICollection<HoaDon>? HoaDons { get; set; }
        public ICollection<HoaDonChiTiet>? HoaDonChiTiets { get; set; } 
    }
}
