using Models.DTOs;
using Models.Entities;

namespace Core.InterfacesRepositories;
public interface IFoodRepository
{
    Task<FoodDto?> GetFoodByIdAsync(int id);
    Task<IEnumerable<FoodDto>> GetFoodsAsync();
    Task<IEnumerable<Food>> GetFoodsByIngredientAsync(int ingredientId);
}

