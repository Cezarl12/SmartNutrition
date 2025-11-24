using Models.Entities;

namespace Core.InterfacesRepositories
{
    public interface IDailyLogRepository
    {
        Task AddDailyLogAsync(DailyLog dailyLog);
        Task<DailyLog?> GetLogByIdAsync(int id);
        Task<List<DailyLog>> GetDailyLogsByUserIdAsync(string userId, int pageNumber, int pageSize);
        Task<List<DailyLog>> GetLogsByDateRangeAsync(string userId, DateTime startDate, DateTime endDate, int pageNumber, int pageSize);
        Task<List<DailyLog>> GetLogsByDateRangeAsync(string userId, DateTime startDate, DateTime endDate);
        void Delete(DailyLog dailyLog);
        Task<bool> SaveChangesAsync();
        Task<bool> UserHasLogForDateAsync(string userId, DateTime date);
    }
}
