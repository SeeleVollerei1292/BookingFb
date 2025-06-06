namespace Mvc.Areas.Admin.Models
{
    public class Nuocuong
    {
        public int Id { get; set; } // Mã nước uống, có thể là duy nhất
        public string TenNuocUong { get; set; } // Tên nước uống, có thể là duy nhất
        public int Soluong { get; set; } // Số lượng nước uống, có thể là số nguyên dương
        public string Anh { get; set; } // Đường dẫn đến ảnh của nước uống, nếu có
        public decimal GiaBan { get; set; } // Giá bán của nước uống
        public bool IsActive { get; set; } // Trạng thái hoạt động của nước uống, mặc định là true
        public string GhiChu { get; set; } // Ghi chú về nước uống, nếu cần
    }
}
