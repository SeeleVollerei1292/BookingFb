namespace bookingfootball.Db_QL
{
    public class NgayTrongTuan
    {
        public int Id { get; set; } // Mã ngày trong tuần, có thể là duy nhất
        public int ThuTu { get; set; } // Thứ tự của ngày trong tuần, ví dụ: 1 cho Thứ Hai, 2 cho Thứ Ba, v.v.
        public bool IsActive { get; set; } = true; // Trạng thái hoạt động của ngày trong tuần, mặc định là true
        public ICollection<SanCa> SanCas { get; set; }
    }
}
