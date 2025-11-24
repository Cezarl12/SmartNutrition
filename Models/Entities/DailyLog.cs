using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class DailyLog
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow.Date;

        public double TotalKcal { get; set; }
        public double TotalProtein { get; set; }
        public double TotalCarbs { get; set; }
        public double TotalFat { get; set; }

        public ICollection<DailyLogMeal> Meals { get; set; } = new List<DailyLogMeal>();

        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }

}