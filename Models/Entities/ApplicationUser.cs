using Microsoft.AspNetCore.Identity;

namespace Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public Sex Sex { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public ActivityLevel ActivityLevel { get; set; }
        public Goal Goal { get; set; }
        public Preference Preferences { get; set; }
        public int NumberOfMeals { get; set; }
        public Target? Target { get; set; }
    }

    public class Target
    {
        public double DailyKcal { get; set; }
        public double DailyProtein { get; set; }
        public double DailyCarbs { get; set; }
        public double DailyFat { get; set; }
    }

    public enum Sex
    {
        Female = 1,
        Male = 2
    }

    public enum ActivityLevel
    {
        Sedentary = 1,
        Light = 2,
        Moderate = 3,
        Active = 4
    }

    public enum Goal
    {
        Lose = 1,
        Maintain = 2,
        Gain = 3
    }
    public enum Preference
    {
        Standard = 1,
        Vegetarian = 2,
        Vegan = 3,
        Keto = 4,
    }
}