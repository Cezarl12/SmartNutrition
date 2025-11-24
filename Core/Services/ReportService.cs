using Core.InterfacesRepositories;
using Core.InterfacesServices;
using Models.DTOs;
using Models.Entities;

namespace Core.Services
{
    public class ReportService(IDailyLogRepository dailyLogRepository) : IReportService
    {
        public async Task<AverageSummaryDto> GetAverageSummaryAsync(ApplicationUser user, DateTime startDate, DateTime endDate)
        {
            var logs = await dailyLogRepository.GetLogsByDateRangeAsync(user.Id, startDate, endDate);

            int numberOfDays = logs.Count;
            if (numberOfDays == 0)
            {
                return new AverageSummaryDto { TargetKcal = user.Target?.DailyKcal ?? 0, TargetProtein = user.Target?.DailyProtein ?? 0 };
            }

            double avgKcal = logs.Average(l => l.TotalKcal);
            double avgProtein = logs.Average(l => l.TotalProtein);
            double avgCarbs = logs.Average(l => l.TotalCarbs);
            double avgFat = logs.Average(l => l.TotalFat);

            double targetKcal = user.Target?.DailyKcal ?? 0;
            double targetProtein = user.Target?.DailyProtein ?? 0;
            double targetCarbs = user.Target?.DailyCarbs ?? 0;
            double targetFat = user.Target?.DailyFat ?? 0;

            return new AverageSummaryDto
            {
                AverageKcalConsumed = Math.Round(avgKcal),
                AverageProteinConsumed = Math.Round(avgProtein),
                AverageCarbsConsumed = Math.Round(avgCarbs),
                AverageFatConsumed = Math.Round(avgFat),
                TargetKcal = Math.Round(targetKcal),
                TargetProtein = Math.Round(targetProtein),
                TargetCarbs = Math.Round(targetCarbs),
                TargetFat = Math.Round(targetFat),
                CalorieDeficitOrSurplus = Math.Round(avgKcal - targetKcal)
            };
        }

        public async Task<List<DailyDataDto>> GetDailyDataAsync(ApplicationUser user, DateTime startDate, DateTime endDate)
        {
            var logs = await dailyLogRepository.GetLogsByDateRangeAsync(user.Id, startDate, endDate);

            return logs.Select(l => new DailyDataDto
            {
                Date = l.Date,
                TotalKcal = l.TotalKcal,
                TotalProtein = l.TotalProtein,
                TotalCarbs = l.TotalCarbs,
                TotalFat = l.TotalFat
            }).ToList();
        }
    }
}

