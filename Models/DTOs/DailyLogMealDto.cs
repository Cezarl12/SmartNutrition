using System.ComponentModel.DataAnnotations;

namespace Models.DTOs
{
    public class DailyLogMealDto
    {
        public int TotalCalories { get; set; }

        [Required]
        public string MealName { get; set; } = string.Empty;

        [Required]
        public List<DailyLogRecipeDto> Recipes { get; set; } = new List<DailyLogRecipeDto>();
    }
}
