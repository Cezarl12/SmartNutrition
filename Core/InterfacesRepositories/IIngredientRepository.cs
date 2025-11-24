using Models.Entities;

namespace Core.InterfacesRepositories
{
    public interface IIngredientRepository
    {
        Task<Ingredient?> GetIngredientByIdAsync(int id);
        Task<IEnumerable<Ingredient>> GetIngredientsAsync();
    }
}
