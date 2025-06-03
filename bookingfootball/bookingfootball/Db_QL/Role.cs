namespace bookingfootball.Db_QL
{
    public enum Role
    {
        Admin,        // Quản trị viên, có quyền cao nhất   
        Staff,        // Nhân viên, có quyền hạn chế hơn so với quản lý
        Customer      // Khách hàng, có quyền đặt sân và xem thông tin
    }
}
