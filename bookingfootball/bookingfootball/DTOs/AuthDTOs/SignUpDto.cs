using bookingfootball.Db_QL;
using System.ComponentModel.DataAnnotations;

namespace bookingfootball.DTOs.AuthDTOs
{
    public class SignUpDto
    {
        public string TenDangNhap { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string SoDienThoai { get; set; }
        public string HoTen { get; set; }
        public string? GioiTinh { get; set; } // For Customer
        public string? Address { get; set; } // For Staff
        public string? IdentityNumber { get; set; } // For Staff
        public Role? Role { get; set; } // Optional, defaults to Customer
    }
}