 namespace bookingfootball.Db_QL
{
    public class Thue
    {
        public int Id { get; set; } // Mã thuế, có thể là duy nhất
        public string TenThue { get; set; } // Tên thuế, có thể là duy nhất
        public int Soluong { get; set; } // Số lượng thuế, có thể là số nguyên dương
        public string DonGia { get; set; } // Đơn giá của thuế, có thể là một chuỗi mô tả
        public bool IsActive { get; set; } = true; // Trạng thái hoạt động của thuế, mặc định là true
    }
}
