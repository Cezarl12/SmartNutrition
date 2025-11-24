using Core.InterfacesRepositories;
using Core.InterfacesServices;
using Models.DTOs;
using Models.Entities;

namespace Core.Services
{
    public class DailyLogService(IFoodRepository foodRepository) : IDailyLogService
    {
        public async Task<DailyLog> CreateDailyLog(DailyLogDto logDto, string userId)
        {
            double dayTotalKcal = 0;
            double dayTotalProtein = 0;
            double dayTotalCarbs = 0;
            double dayTotalFat = 0;

            var mealsToSave = new List<DailyLogMeal>();
            foreach (var mealDto in logDto.Meals)
            {
                double mealTotalKcal = 0;
                var newMeal = new DailyLogMeal
                {
                    MealName = mealDto.MealName,
                    Recipes = new List<DailyLogRecipe>(),
                };

                foreach (var recipeDto in mealDto.Recipes)
                {
                    var foodEntity = await foodRepository.GetFoodByIdAsync(recipeDto.FoodId);

                    if (foodEntity == null || foodEntity.StandardServingSizeInGrams <= 0)
                        throw new InvalidOperationException($"Cannot calculate multiplier for Food ID {recipeDto.FoodId}. Standard serving size is missing.");

                    double multiplier = recipeDto.QuantityConsumedInGrams / foodEntity.StandardServingSizeInGrams;
                    double recipeKcal = foodEntity.Kcal * multiplier;
                    dayTotalProtein += foodEntity.Protein * multiplier;
                    dayTotalCarbs += foodEntity.Carbs * multiplier;
                    dayTotalFat += foodEntity.Fat * multiplier;

                    mealTotalKcal += recipeKcal;
                    dayTotalKcal += recipeKcal;

                    newMeal.Recipes.Add(new DailyLogRecipe
                    {
                        FoodId = recipeDto.FoodId,
                        ConsumptionMultiplier = multiplier,
                        imageUrl = recipeDto.imageUrl,
                        Name = recipeDto.Name,
                        QuantityConsumedInGrams = recipeDto.QuantityConsumedInGrams
                    });

                }

                newMeal.TotalCalories = (int)Math.Round(mealTotalKcal);

                mealsToSave.Add(newMeal);
            }

            var dailyLog = new DailyLog
            {
                Date = logDto.Date.Date,
                ApplicationUserId = userId,
                TotalKcal = Math.Round(dayTotalKcal),
                TotalProtein = Math.Round(dayTotalProtein),
                TotalCarbs = Math.Round(dayTotalCarbs),
                TotalFat = Math.Round(dayTotalFat),

                Meals = mealsToSave
            };
            return dailyLog;
        }
    }
}
