using System.ComponentModel.DataAnnotations;

namespace Models.DTOs
{
    public class DailyLogDto
    {

        [Required]
        public DateTime Date { get; set; } = DateTime.UtcNow.Date;

        [Required]
        public List<DailyLogMealDto> Meals { get; set; } = new List<DailyLogMealDto>();
    }

}

