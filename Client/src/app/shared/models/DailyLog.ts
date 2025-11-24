export interface DailyLog {
    id: number
    date: string;
    meals: DailyLogMeal[];
    totalKcal: number;
    totalProtein: number;
    totalCarbs: number;
    totalFat: number;
}

export interface DailyLogDto {
    date: string;
    meals: DailyLogMeal[];
}

export interface DailyLogMeal {
    mealName: string | null;
    recipes: DailyLogRecipe[];
    totalCalories: number | null;
}

export interface DailyLogRecipe {
    foodId: number;
    name: string | null;
    quantityConsumedInGrams: number;
    imageUrl: string | null;
}