namespace Models.DTOs
{
    public class IngredientDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double QuantityInGrams { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class FoodDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public double StandardServingSizeInGrams { get; set; }

        public bool IsVegan { get; set; }
        public bool IsVegetarian { get; set; }

        public double Kcal { get; set; }
        public double Protein { get; set; }
        public double Carbs { get; set; }
        public double Fat { get; set; }

        public List<IngredientDto> Ingredients { get; set; } = new();
    }

}
