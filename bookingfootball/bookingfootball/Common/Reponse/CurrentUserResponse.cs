using bookingfootball.Db_QL;

namespace bookingfootball.Common.Reponse
{
    public class CurrentUserResponse
    {
        public int Id { get; set; }
        public string TenDangNhap { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string HoTen { get; set; }
        public Role Role { get; set; }
    }
}
