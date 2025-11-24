using Models.Entities;

namespace Core.InterfacesServices
{
    public interface INutritionService
    {
        Target CalculateDailyTargets(ApplicationUser user);
    }
}
