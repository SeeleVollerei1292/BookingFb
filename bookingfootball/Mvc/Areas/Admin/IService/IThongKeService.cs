using bookingfootball.DTOs;
using Mvc.Models;

namespace Mvc.Areas.Admin.IService
{
    public interface IThongKeService
    {
        Task<ThongKeViewModel> GetThongKeAsync();
        Task<ThongKeDTO> FilterStatisticsAsync(DateTime? fromDate, DateTime? toDate);
    }
}

