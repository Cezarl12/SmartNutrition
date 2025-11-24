using System.ComponentModel.DataAnnotations;

namespace Models.DTOs
{
    public class DailyLogRecipeDto
    {
        public string? Name { get; set; }

        public string? imageUrl { get; set; }

        [Required]
        public int FoodId { get; set; }

        public double QuantityConsumedInGrams { get; set; } = 1.0;
    }
}
