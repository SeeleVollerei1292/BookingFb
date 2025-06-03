namespace bookingfootball.Db_QL
{
    public class LichLamViec
    {   
        public int Id { get; set; } // Mã lịch làm việc, có thể là duy nhất
        public int NhanVienId { get; set; } // Mã nhân viên, liên kết với bảng NhanVien
        public NhanVien NhanVien { get; set; } // Thông tin nhân viên liên kết
        public string ViTri { get; set; } // Vị trí làm việc, có thể là duy nhất
        public DateTime Ngay { get; set; } // Ngày làm việc
        public DateTime ThoiGianBatDau { get; set; } // Thời gian bắt đầu làm việc
        public DateTime ThoiGianKetThuc { get; set; } // Thời gian kết thúc làm việc
        public bool IsActive { get; set; } = true; // Trạng thái hoạt động của lịch làm việc, mặc định là true
        public string GhiChu { get; set; } // Ghi chú về lịch làm việc, có thể để trống
    }
}
