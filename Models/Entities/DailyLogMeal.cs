using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models.Entities
{
    public class DailyLogMeal
    {
        public int Id { get; set; }

        public int TotalCalories { get; set; }

        public string MealName { get; set; } = string.Empty;

        public ICollection<DailyLogRecipe> Recipes { get; set; } = new List<DailyLogRecipe>();

        public int DailyLogId { get; set; }

        [JsonIgnore]
        [ForeignKey("DailyLogId")]
        public DailyLog DailyLog { get; set; }
    }
}