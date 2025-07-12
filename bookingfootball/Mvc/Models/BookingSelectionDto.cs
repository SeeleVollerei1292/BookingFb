namespace Mvc.Models
{
    public class BookingSelectionDto
    {
        public int SanBongId { get; set; }
        public string TenSan { get; set; }
        public int Gia { get; set; }
        public List<string> Dates { get; set; } = new List<string>();
        public List<CaSelectionDto> Cas { get; set; } = new List<CaSelectionDto>();
        public string GhiChu { get; set; }
        public decimal TongTien { get; set; }
    }
}

