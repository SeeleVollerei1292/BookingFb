using bookingfootball.DTOs;

namespace bookingfootball.IRepository
{
    public interface IThongKeRepository
    {
        Task<List<DoanhThuTheoSanDTO>> GetDoanhThuTheoSan();
        Task<List<DoanhThuTheoNgayDTO>> GetDoanhThuTheoNgay();
        Task<List<DoanhThuTheoThangDTO>> GetDoanhThuTheoThang();
        Task<List<DoanhThuTheoNamDTO>> GetDoanhThuTheoNam();
        Task<ThongKeSuDungSanDTO> GetThongKeSuDungSan();

        // Thống kê theo khoảng thời gian
        Task<ThongKeDTO> GetFilteredStatistics(DateTime? fromDate, DateTime? toDate);
    }
}
