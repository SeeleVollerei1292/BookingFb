namespace Mvc.Models
{
    public class LichSuDatSanViewModel
    {
        public int HoaDonId { get; set; }
        public string MaHoaDon { get; set; }
        public DateTime NgayLap { get; set; }
        public decimal TongTien { get; set; }
        public decimal? TienCoc { get; set; }
        public string TenNguoiDat { get; set; }
        public string Email { get; set; }
        public int? SoDienThoaiNguoiDat { get; set; }
        public string TrangThaiThanhToan { get; set; }
        public string TrangThaiHoaDon { get; set; }
        public string VNPayTransactionId { get; set; }
        public List<ChiTietDatSan> ChiTietDatSans { get; set; } = new List<ChiTietDatSan>();

        // Chuyển đổi TrangThaiHoaDon thành văn bản thân thiện
        public string GetFriendlyTrangThaiHoaDon()
        {
            return TrangThaiHoaDon switch
            {
                "ChuaThanhToan" => "Chưa thanh toán",
                "DaThanhToan" => "Đã thanh toán",
                "DaHuy" => "Đã hủy",
                "ThanhToanTrucTiep" => "Thanh toán trực tiếp",
                _ => "Không xác định"
            };
        }

        // Chuyển đổi TrangThaiThanhToan thành văn bản thân thiện
        public string GetFriendlyTrangThaiThanhToan()
        {
            return TrangThaiThanhToan switch
            {
                "Success" => "Thành công",
                "Failed" => "Thất bại",
                null => "Chưa xử lý",
                _ => TrangThaiThanhToan ?? "Không xác định"
            };
        }

        public class ChiTietDatSan
        {
            public int HoaDonChiTietId { get; set; }
            public string MaChiTietHoaDon { get; set; }
            public string TenSanBong { get; set; }
            public string TenCa { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public DateTime NgayDenSan { get; set; }
            public decimal TienThueSan { get; set; }
            public decimal? TongTienDuocGiam { get; set; }
            public decimal TongTienChiTiet { get; set; }
            public string GhiChu { get; set; }
            public List<DichVuDat> DichVus { get; set; } = new List<DichVuDat>();
        }

        public class DichVuDat
        {
            public string TenDichVu { get; set; } // Nước uống hoặc đồ thuê
            public int SoLuong { get; set; }
            public decimal DonGia { get; set; }
            public decimal TongTien { get; set; }
        }
    }
}