namespace Mvc.Models
{
    public class CaSelectionDto
    {
        public int CaId { get; set; }
        public int SanCaId { get; set; }
        public string TenCa { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Date { get; set; }
        public List<NuocUongSelectionDto> NuocUongSelections { get; set; } = new List<NuocUongSelectionDto>();
        public List<DoThueSelectionDto> DoThueSelections { get; set; } = new List<DoThueSelectionDto>();
    }
}
