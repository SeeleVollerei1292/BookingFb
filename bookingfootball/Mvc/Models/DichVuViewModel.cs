namespace Mvc.Models
{
    public class DichVuViewModel
    {
        public string? TenNuocUong { get; set; }
        public string? TenDoThue { get; set; }
        public string? TenThueSan { get; set; }
        public int ? SoLuong { get; set; }
        public int? SoLuongDoThue { get; set; } // Số lượng đồ thuê, nếu có
        public decimal TongTien { get; set; }
        public string? GhiChu { get; set; }
    }
}
