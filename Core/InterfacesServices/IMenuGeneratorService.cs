using Models.DTOs;
using Models.Entities;

namespace Core.InterfacesServices
{
    public interface IMenuGeneratorService
    {
        public Task<List<GeneratedMealDto>> GenerateDailyMenuAsync(ApplicationUser user);
    }
}
