namespace bookingfootball.Db_QL
{
    public class ThoiGianDatSan
    {
        public int Id { get; set; }
        public DateTime NgayDat{ get; set; }
        public int IdHoaDon { get; set; }
        public HoaDon HoaDon { get; set; }
        public int IdSanCa { get; set; }
        public SanCa SanCa { get; set; }
    
    }
}
