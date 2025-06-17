namespace bookingfootball.Db_QL
{
    public class SanCa
    {
        public int Id { get; set; } // Mã sân ca, có thể là duy nhất
        public int SanBongId { get; set; } // Mã sân bóng, liên kết với bảng SanBong
        public Sanbong SanBong { get; set; } // Thông tin sân bóng liên kết
        public int CaId { get; set; } // Mã ca, liên kết với bảng Ca
        public int NgayTrongTuanId { get; set; } // Mã ngày trong tuần, liên kết với bảng NgayTrongTuan
        public NgayTrongTuan NgayTrongTuan { get; set; } // Thông tin ngày trong tuần liên kết
        public Ca Ca { get; set; } // Thông tin ca liên kết
        //public decimal Gia { get; set; } // Giá của sân ca, có thể là duy nhất

        public bool IsActive { get; set; } = true; // Trạng thái hoạt động của sân ca, mặc định là true
    }
}
