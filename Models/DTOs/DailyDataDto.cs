namespace Models.DTOs
{
    public class DailyDataDto
    {
        public DateTime Date { get; set; }
        public double TotalKcal { get; set; }
        public double TotalProtein { get; set; }
        public double TotalCarbs { get; set; }
        public double TotalFat { get; set; }
    }
}
