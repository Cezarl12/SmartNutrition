using Models.Entities;

namespace Models.DTOs
{
    public class UserInfo
    {
        public string? Email { get; set; }
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

        public Target Target { get; set; } = new Target();
    }
}
