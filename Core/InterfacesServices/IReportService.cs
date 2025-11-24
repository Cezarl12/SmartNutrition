using Models.DTOs;
using Models.Entities;

namespace Core.InterfacesServices
{
    public interface IReportService
    {
        Task<AverageSummaryDto> GetAverageSummaryAsync(ApplicationUser user, DateTime startDate, DateTime endDate);

        Task<List<DailyDataDto>> GetDailyDataAsync(ApplicationUser user, DateTime startDate, DateTime endDate);
    }
}
