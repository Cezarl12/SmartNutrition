using Core.InterfacesRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Infrastructure.Repositories
{
    public class DailyLogRepository(ApplicationDbContext context) : IDailyLogRepository
    {
        public async Task AddDailyLogAsync(DailyLog dailyLog)
        {
            await context.DailyLogs.AddAsync(dailyLog);
        }

        public void Delete(DailyLog dailyLog)
        {
            context.DailyLogs.Remove(dailyLog);
        }

        public async Task<List<DailyLog>> GetDailyLogsByUserIdAsync(string userId, int pageNumber, int pageSize)
        {
            return await context.DailyLogs
            .Where(dl => dl.ApplicationUserId == userId)
            .Include(dl => dl.Meals).ThenInclude(meal => meal.Recipes).ThenInclude(recipe => recipe.Food)
            .OrderByDescending(dl => dl.Date)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        }

        public async Task<DailyLog?> GetLogByIdAsync(int id)
        {
            return await context.DailyLogs.Include(dl => dl.Meals).ThenInclude(meal => meal.Recipes).ThenInclude(recipe => recipe.Food)
                    .FirstOrDefaultAsync(dl => dl.Id == id);
        }

        public async Task<List<DailyLog>> GetLogsByDateRangeAsync(string userId, DateTime startDate, DateTime endDate, int pageNumber, int pageSize)
        {
            return await context.DailyLogs
                .Where(dl => dl.ApplicationUserId == userId && dl.Date >= startDate.Date && dl.Date <= endDate.Date)
                .Include(dl => dl.Meals).ThenInclude(meal => meal.Recipes).ThenInclude(recipe => recipe.Food)
                .OrderByDescending(dl => dl.Date)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

        }

        public async Task<List<DailyLog>> GetLogsByDateRangeAsync(string userId, DateTime startDate, DateTime endDate)
        {
            return await context.DailyLogs
               .Where(dl => dl.ApplicationUserId == userId && dl.Date >= startDate.Date && dl.Date <= endDate.Date)
               .Include(dl => dl.Meals).ThenInclude(meal => meal.Recipes).ThenInclude(recipe => recipe.Food)
               .OrderByDescending(dl => dl.Date)
               .ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }


        public async Task<bool> UserHasLogForDateAsync(string userId, DateTime date)
        {
            var startOfDay = date.Date;
            var endOfDay = date.Date.AddDays(1).AddTicks(-1);

            return await context.DailyLogs
          .AnyAsync(l =>
              l.ApplicationUserId == userId &&
              l.Date >= startOfDay &&
              l.Date <= endOfDay);
        }

    }
}
