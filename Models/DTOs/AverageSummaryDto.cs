namespace Models.DTOs
{
    public class AverageSummaryDto
    {
        public double AverageKcalConsumed { get; set; }
        public double AverageProteinConsumed { get; set; }
        public double AverageCarbsConsumed { get; set; }
        public double AverageFatConsumed { get; set; }

        public double TargetKcal { get; set; }
        public double TargetProtein { get; set; }
        public double TargetCarbs { get; set; }
        public double TargetFat { get; set; }

        public double CalorieDeficitOrSurplus { get; set; }
    }
}
