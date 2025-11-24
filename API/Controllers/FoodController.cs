using Core.InterfacesRepositories;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController(IFoodRepository foodRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Food>>> GetFoods()
        {
            var foods = await foodRepository.GetFoodsAsync();
            return Ok(foods);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Food>> GetFoodById(int id)
        {
            var food = await foodRepository.GetFoodByIdAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            return Ok(food);
        }

        [HttpGet("by-ingredient/{ingredientId}")]
        public async Task<ActionResult<IEnumerable<Food>>> GetFoodsByIngredient(int ingredientId)
        {
            var foods = await foodRepository.GetFoodsByIngredientAsync(ingredientId);
            return Ok(foods);
        }

    }
}
