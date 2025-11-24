namespace Models.DTOs
{
    public class GeneratedMealDto
    {
        public string MealName { get; set; } = string.Empty;

        public double TargetKcal { get; set; }
        public double ActualKcal { get; set; }

        public List<FoodDto> Recipes { get; set; } = new List<FoodDto>();
    }
}
