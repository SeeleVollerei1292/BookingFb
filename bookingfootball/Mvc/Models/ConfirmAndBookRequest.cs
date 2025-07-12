namespace Mvc.Models
{
    public class ConfirmAndBookRequest
    {
        public int SanBongId { get; set; }
        public List<string> Dates { get; set; }
        public List<int> CaIds { get; set; }
        public List<int> SanCaIds { get; set; }
        public List<BookingSelectionDto> Selections { get; set; }
        public string GhiChu { get; set; }
    }
}
