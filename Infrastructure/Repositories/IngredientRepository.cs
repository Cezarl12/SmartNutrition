using Core.InterfacesRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Infrastructure.Repositories
{
    public class IngredientRepository(ApplicationDbContext context) : IIngredientRepository
    {
        public async Task<Ingredient?> GetIngredientByIdAsync(int id)
        {
            return await context.Ingredients.FindAsync(id);
        }

        public async Task<IEnumerable<Ingredient>> GetIngredientsAsync()
        {
            var query = context.Ingredients.AsQueryable();
            return await query.ToListAsync();
        }
    }
}
