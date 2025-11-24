using Core.InterfacesRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Entities;

namespace Infrastructure.Repositories
{
    public class FoodRepository(ApplicationDbContext context) : IFoodRepository
    {
        public async Task<FoodDto?> GetFoodByIdAsync(int id)
        {
            return await context.Foods
         .Include(f => f.Ingredients)
             .ThenInclude(iq => iq.Ingredient)
         .Where(f => f.Id == id)
         .Select(f => new FoodDto
         {
             Id = f.Id,
             Name = f.Name,
             Description = f.Description,
             ImageUrl = f.ImageUrl,
             StandardServingSizeInGrams = f.StandardServingSizeInGrams,
             Kcal = f.Kcal,
             Protein = f.Protein,
             IsVegan = f.IsVegan,
             IsVegetarian = f.IsVegetarian,
             Carbs = f.Carbs,
             Fat = f.Fat,
             Ingredients = f.Ingredients.Select(iq => new IngredientDto
             {
                 Id = iq.IngredientId,
                 Name = iq.Ingredient.Name,
                 QuantityInGrams = iq.QuantityInGrams,
                 ImageUrl = iq.Ingredient.ImageUrl
             }).ToList()
         })
     .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<FoodDto>> GetFoodsAsync()
        {
            return await context.Foods
                .Include(f => f.Ingredients)
                    .ThenInclude(iq => iq.Ingredient)
                .Select(f => new FoodDto
                {
                    Id = f.Id,
                    Name = f.Name,
                    Description = f.Description,
                    ImageUrl = f.ImageUrl,
                    StandardServingSizeInGrams = f.StandardServingSizeInGrams,
                    Kcal = f.Kcal,
                    Protein = f.Protein,
                    IsVegan = f.IsVegan,
                    IsVegetarian = f.IsVegetarian,
                    Carbs = f.Carbs,
                    Fat = f.Fat,
                    Ingredients = f.Ingredients.Select(iq => new IngredientDto
                    {
                        Id = iq.IngredientId,
                        Name = iq.Ingredient.Name,
                        QuantityInGrams = iq.QuantityInGrams,
                        ImageUrl = iq.Ingredient.ImageUrl
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<Food>> GetFoodsByIngredientAsync(int ingredientId)
        {
            var foods = await context.Foods.Where(f => f.Ingredients.Any(fi => fi.IngredientId == ingredientId)).ToListAsync();
            return foods;
        }

    }
}
