namespace bookingfootball.Db_QL
{
    public class LoaiSan
    {
        public int Id { get; set; }
        public string TenLoaiSan { get; set; }
        public string MoTa { get; set; }
        public string? HinhAnh { get; set; }
        public bool TrangThai { get; set; } = true;
        public ICollection<Sanbong>? SanBongs { get; set; }
    }
}
