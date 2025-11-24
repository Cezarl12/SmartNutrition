using Models.DTOs;
using Models.Entities;

namespace Core.InterfacesServices
{
    public interface IDailyLogService
    {
        public Task<DailyLog> CreateDailyLog(DailyLogDto logDto, string userId);

    }
}
