namespace bookingfootball.Db_QL
{
    public class DiaChiKhachHang
    {
        public int Id { get; set; }
        public int KhachHangId { get; set; }
        public KhachHang KhachHang { get; set; }
        public string TenDiaChi { get; set; }
        public string ChiTietDiaChi { get; set; }
        public string ThanhPho { get; set; }
        public string QuanHuyen { get; set; }
        public string PhuongXa { get; set; }
        public bool IsActive { get; set; } = true;
        public string GhiChu { get; set; }
    }
}
