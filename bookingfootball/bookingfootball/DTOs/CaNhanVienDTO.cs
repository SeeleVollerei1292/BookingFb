namespace bookingfootball.DTOs
{
    public class CaNhanVienDTO
    {
        public int Id { get; set; }
        public int NhanVienId { get; set; }
        public string? TenNhanVien { get; set; }
        public string ViTri { get; set; }
        public string? GhiChu { get; set; }
        public DateTime Ngay { get; set; }
        public TimeSpan ThoiGianBatDau { get; set; }
        public TimeSpan ThoiGianKetThuc { get; set; }
    }
}
