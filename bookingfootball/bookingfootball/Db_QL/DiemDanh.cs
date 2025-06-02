namespace bookingfootball.Db_QL
{
    public class DiemDanh
    {
        public int Id { get; set; } // Mã điểm danh, có thể là duy nhất
        public int NhanVienId { get; set; } // Mã nhân viên, liên kết với bảng NhanVien
        public NhanVien NhanVien { get; set; } // Thông tin nhân viên liên kết
        public string ChucVu { get; set; } // Chức vụ của nhân viên, có thể là duy nhất
        public DateTime GioBatDau { get; set; } // Thời gian bắt đầu làm việc
        public DateTime GioKetThuc { get; set; } // Thời gian kết thúc làm việc
        public bool IsActive { get; set; } = true; // Trạng thái hoạt động của điểm danh, mặc định là true
    }
}
