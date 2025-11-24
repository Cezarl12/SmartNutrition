using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class FoodIngredient
    {
        public int Id { get; set; }
        public double QuantityInGrams { get; set; }

        public int FoodId { get; set; }
        [ForeignKey("FoodId")]
        public Food Food { get; set; }

        public int IngredientId { get; set; }
        [ForeignKey("IngredientId")]
        public Ingredient Ingredient { get; set; }
    }
}
