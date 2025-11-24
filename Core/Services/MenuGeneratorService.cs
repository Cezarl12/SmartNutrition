using Core.InterfacesRepositories;
using Core.InterfacesServices;
using Models.DTOs;
using Models.Entities;

namespace Core.Services
{
    public class MenuGeneratorService(IFoodRepository foodRepository, Random random) : IMenuGeneratorService
    {


        public async Task<List<GeneratedMealDto>> GenerateDailyMenuAsync(ApplicationUser user)
        {
            if (user.Target == null || user.Target.DailyKcal <= 0 || user.NumberOfMeals < 1)
                throw new InvalidOperationException("User profile incomplete or targets not calculated.");


            var allRecipes = (await foodRepository.GetFoodsAsync()).ToList();

            var availableRecipes = FilterRecipesByPreference(allRecipes, user.Preferences).ToList();

            var mealDistribution = GetMealDistribution(user.NumberOfMeals, user.Target.DailyKcal);

            var dailyMenu = new List<GeneratedMealDto>();
            var recipesUsed = new HashSet<int>();


            foreach (var (mealName, targetKcal) in mealDistribution)
            {
                var mealDto = new GeneratedMealDto { MealName = mealName, TargetKcal = targetKcal };

                double mealProteinTarget = user.Target.DailyProtein * (targetKcal / user.Target.DailyKcal);
                double mealCarbTarget = user.Target.DailyCarbs * (targetKcal / user.Target.DailyKcal);
                double mealFatTarget = user.Target.DailyFat * (targetKcal / user.Target.DailyKcal);

                var candidates = availableRecipes
                    .Where(r => !recipesUsed.Contains(r.Id))
                    .ToList();

                if (candidates.Any())
                {
                    var combo = GetBestRecipeCombination(candidates, targetKcal, mealProteinTarget, mealCarbTarget, mealFatTarget, random);

                    if (combo.Any())
                    {
                        mealDto.Recipes.AddRange(combo);
                        mealDto.ActualKcal = combo.Sum(r => r.Kcal);
                        foreach (var r in combo)
                            recipesUsed.Add(r.Id);
                    }
                    else
                    {
                        mealDto.ActualKcal = 0;
                    }
                }
                dailyMenu.Add(mealDto);
            }
            return dailyMenu;
        }


        private List<FoodDto> FilterRecipesByPreference(List<FoodDto> recipes, Preference preference)
        {
            switch (preference)
            {
                case Preference.Vegan:
                return recipes.Where(r => r.IsVegan).ToList();
                case Preference.Vegetarian:
                return recipes.Where(r => r.IsVegetarian).ToList();
                default:
                return recipes;
            }

        }

        private List<(string Name, double KcalTarget)> GetMealDistribution(int numMeals, double dailyKcal)
        {
            var distribution = new List<(string, double)>();
            if (numMeals == 1)
            {
                distribution.Add(("Prânz", dailyKcal));
            }
            else if (numMeals == 2)
            {
                distribution.Add(("Prânz", dailyKcal * 0.50));
                distribution.Add(("Cină", dailyKcal * 0.50));
            }
            else if (numMeals == 3)
            {
                distribution.Add(("Mic Dejun", dailyKcal * 0.30));
                distribution.Add(("Prânz", dailyKcal * 0.40));
                distribution.Add(("Cină", dailyKcal * 0.30));
            }
            else if (numMeals == 4)
            {
                distribution.Add(("Mic Dejun", dailyKcal * 0.30));
                distribution.Add(("Prânz", dailyKcal * 0.35));
                distribution.Add(("Gustare PM", dailyKcal * 0.10));
                distribution.Add(("Cină", dailyKcal * 0.25));
            }
            else if (numMeals == 5)
            {
                distribution.Add(("Mic Dejun", dailyKcal * 0.25));
                distribution.Add(("Gustare AM", dailyKcal * 0.10));
                distribution.Add(("Prânz", dailyKcal * 0.35));
                distribution.Add(("Gustare PM", dailyKcal * 0.10));
                distribution.Add(("Cină", dailyKcal * 0.20));
            }
            return distribution;
        }
        private List<FoodDto> GetBestRecipeCombination(List<FoodDto> available, double targetKcal, double targetProtein, double targetCarbs, double targetFat, Random random)
        {
            var bestCombo = new List<FoodDto>();
            double bestScore = double.MaxValue;

            for (int i = 0; i < 2000; i++)
            {
                int comboSize = random.Next(1, 4);
                var combo = available.OrderBy(_ => random.Next()).Take(comboSize).ToList();

                double kcal = combo.Sum(r => r.Kcal);
                double protein = combo.Sum(r => r.Protein);
                double carbs = combo.Sum(r => r.Carbs);
                double fat = combo.Sum(r => r.Fat);

                double kcalDiff = Math.Abs(kcal - targetKcal);
                double proteinDiff = Math.Abs(protein - targetProtein);
                double carbDiff = Math.Abs(carbs - targetCarbs);
                double fatDiff = Math.Abs(fat - targetFat);

                double score = kcalDiff + proteinDiff * 4 + carbDiff * 2 + fatDiff * 3;

                if (score < bestScore && kcal > targetKcal * 0.7 && kcal < targetKcal * 1.3)
                {
                    bestScore = score;
                    bestCombo = combo;
                }
            }

            return bestCombo;
        }
    }
}