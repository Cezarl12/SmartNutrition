using Core.InterfacesServices;
using Models.Entities;

namespace Core.Services
{
    public class NutritionService : INutritionService
    {
        public Target CalculateDailyTargets(ApplicationUser user)
        {
            if (user.Height == 0 || user.Weight == 0 || user.Age == 0)
            {
                return new Target
                {
                    DailyKcal = 0,
                    DailyProtein = 0,
                    DailyCarbs = 0,
                    DailyFat = 0
                };
            }
            double bmr = 0;
            if (user.Sex == Sex.Male)
            {
                bmr = (10 * user.Weight) + (6.25 * user.Height) - (5 * user.Age) + 5;
            }
            else
            {
                bmr = (10 * user.Weight) + (6.25 * user.Height) - (5 * user.Age) - 161;
            }

            double tdee = bmr * GetActivityFactor(user.ActivityLevel);

            double finalKcal = tdee + GetGoalAdjustment(user.Goal);


            Target finalTarget = CalculateMacros(finalKcal, user.Weight, user.Goal, user.Preferences);

            return finalTarget;
        }


        private double GetActivityFactor(ActivityLevel activityLevel)
        {
            switch (activityLevel)
            {
                case ActivityLevel.Sedentary:
                return 1.2;
                case ActivityLevel.Light:
                return 1.375;
                case ActivityLevel.Moderate:
                return 1.55;
                case ActivityLevel.Active:
                return 1.725;
                default:
                return 1.2;
            }
        }

        private double GetGoalAdjustment(Goal goal)
        {

            switch (goal)
            {
                case Goal.Lose:
                return -300;
                case Goal.Maintain:
                return 0;
                case Goal.Gain:
                return 300;
                default:
                return 0;
            }
        }

        private Target CalculateMacros(double totalKcal, double userWeight, Goal goal, Preference preference)
        {
            double proteinGrams, fatGrams, carbsGrams;
            double proteinKcal, fatKcal, carbsKcal;


            if (preference == Preference.Keto)
            {
                fatKcal = totalKcal * 0.70;
                proteinKcal = totalKcal * 0.25;
                carbsKcal = totalKcal * 0.05;

                proteinGrams = proteinKcal / 4;
                fatGrams = fatKcal / 9;
                carbsGrams = carbsKcal / 4;
            }
            else
            {

                proteinGrams = 1.8 * userWeight;
                proteinKcal = proteinGrams * 4;

                switch (goal)
                {
                    case Goal.Lose:
                    fatKcal = totalKcal * 0.30;
                    break;
                    case Goal.Gain:
                    fatKcal = totalKcal * 0.20;
                    break;
                    case Goal.Maintain:
                    default:
                    fatKcal = totalKcal * 0.25;
                    break;
                }
                fatGrams = fatKcal / 9;

                carbsKcal = totalKcal - proteinKcal - fatKcal;
                carbsGrams = carbsKcal / 4;

            }

            return new Target
            {
                DailyKcal = Math.Round(totalKcal),
                DailyProtein = Math.Round(proteinGrams),
                DailyCarbs = Math.Round(carbsGrams),
                DailyFat = Math.Round(fatGrams)
            };
        }
    }
}