namespace bookingfootball.Db_QL
{
    public class Ca
    {
        public int Id { get; set; } // Mã ca, có thể là duy nhất
        public string TenCa { get; set; } // Tên ca, có thể là duy nhất
        public DateTime StartTime { get; set; } // Thời gian bắt đầu của ca
        public DateTime EndTime { get; set; } // Thời gian kết thúc của ca
        public bool IsActive { get; set; }
        public ICollection<SanCa> SanCas { get; set; }
    }
}
