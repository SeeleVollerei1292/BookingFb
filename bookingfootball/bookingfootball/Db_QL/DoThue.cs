using bookingfootball.Db_QL;
using System.ComponentModel.DataAnnotations;

namespace Duong_API.Data
{
    public class DoThue
    {
        public int Id { get; set; }
        public string TenDoThue { get; set; }
        public string MoTa { get; set; }
        public string HinhAnh { get; set; } 
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public bool TrangThai { get; set; }
        public int? HoaDonChiTietId { get; set; }
        public HoaDonChiTiet? HoaDonChiTiet { get; set; }
    }
}
