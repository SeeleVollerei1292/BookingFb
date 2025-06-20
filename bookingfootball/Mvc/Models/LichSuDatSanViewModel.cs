namespace Mvc.Models
{
    public class LichSuDatSanViewModel
    {
        public string TenSan { get; set; }
        public decimal Gia { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayDenSan { get; set; }
        public decimal TongTien { get; set; }
        public List<DichVuViewModel> DichVuDatBongList { get; set; }
    }
}
