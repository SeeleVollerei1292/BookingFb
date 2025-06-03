namespace bookingfootball.Db_QL
{
    public class Sanbong
    {
        public int Id { get; set; }
        public string TenSan { get; set; }
        public int LoaiSanId { get; set; } 
        public LoaiSan LoaiSan { get; set; }
        public string MoTa { get; set; }
        public ICollection<SanCa> SanCas { get; set; }
        public ICollection<HoaDonChiTiet> HoaDonChiTiets { get; set; }
    }
}
