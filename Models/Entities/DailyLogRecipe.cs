using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models.Entities
{
    public class DailyLogRecipe
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public double ConsumptionMultiplier { get; set; } = 1.0;

        public double QuantityConsumedInGrams { get; set; }

        public string? imageUrl { get; set; }


        public int FoodId { get; set; }
        [ForeignKey("FoodId")]
        public Food Food { get; set; }

        public int DailyLogMealId { get; set; }

        [JsonIgnore]
        [ForeignKey("DailyLogMealId")]
        public DailyLogMeal DailyLogMeal { get; set; }
    }
}