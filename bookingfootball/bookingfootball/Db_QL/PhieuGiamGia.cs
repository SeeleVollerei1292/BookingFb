namespace bookingfootball.Db_QL
{
    public class PhieuGiamGia
    {
        public int Id { get; set; }
        public string MaPhieu { get; set; }
        public string TenPhieu { get; set; }
        public int SoLuong { get; set; }
        public int SoLuongDaSuDung { get; set; } = 0;
        public decimal GiaTriGiamToiDa { get; set; }
        public string DieuKien { get; set; }

        public decimal GiaTriGiam { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<PhieuGiamGiaChiTiet> PhieuGiamGiaChiTiets { get; set; }

    }
}
