namespace Mvc.Models
{
    public class OrderViewModel
    {
        public int HoaDonId { get; set; }
        public string TenNguoiDat { get; set; }
        public string SoDienThoaiNguoiDat { get; set; }
        public string Email { get; set; }
        public string GhiChu { get; set; }
        public string PaymentUrl { get; set; }
        public BookingData Booking { get; set; } = new BookingData();
    }

    public class BookingData
    {
        public int SanBongId { get; set; }
        public string TenSan { get; set; }
        public decimal Gia { get; set; }
        public List<string> Dates { get; set; } = new List<string>();
        public List<CaData> Cas { get; set; } = new List<CaData>();
        public decimal TongTien { get; set; }
    }

    public class CaData
    {
        public int CaId { get; set; }
        public int? SanCaId { get; set; }
        public string TenCa { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Date { get; set; }
        public bool IsActive { get; set; }
        public List<NuocUongSelection> NuocUongSelections { get; set; } = new List<NuocUongSelection>();
        public List<DoThueSelection> DoThueSelections { get; set; } = new List<DoThueSelection>();
    }

    public class NuocUongSelection
    {
        public int Id { get; set; }
        public string TenNuocUong { get; set; }
        public decimal GiaBan { get; set; }
        public int Quantity { get; set; }
    }

    public class DoThueSelection
    {
        public int Id { get; set; }
        public string TenDoThue { get; set; }
        public decimal DonGia { get; set; }
        public int Quantity { get; set; }
    }
}

