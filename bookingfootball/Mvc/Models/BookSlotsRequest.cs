namespace Mvc.Models
{
    public class BookSlotsRequest
    {
        public int SanBongId { get; set; }
        public List<string> Dates { get; set; }
        public List<int> CaIds { get; set; }
        public List<int> SanCaIds { get; set; } // Added to include specific SanCaIds
        public string Date { get; set; }
    }
}
