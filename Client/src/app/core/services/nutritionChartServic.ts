import { Injectable } from '@angular/core';
import { Food } from '../../shared/models/Food';

export interface MacroChartData {
    caloriesData: number[];
    totalCalories: number;
}

@Injectable({
    providedIn: 'root'
})
export class NutritionChartService {

    private readonly KCAL_PER_PROTEIN = 4;
    private readonly KCAL_PER_CARB = 4;
    private readonly KCAL_PER_FAT = 9;

    public sumMacrosFromRecipes(recipes: Food[]): { p: number, c: number, f: number } {
        let totalProtein = 0;
        let totalCarbs = 0;
        let totalFat = 0;

        for (const recipe of recipes) {
            totalProtein += recipe.protein;
            totalCarbs += recipe.carbs;
            totalFat += recipe.fat;
        }

        return { p: totalProtein, c: totalCarbs, f: totalFat };
    }

    public calculateMacroSplit(proteinGrams: number, carbsGrams: number, fatGrams: number): MacroChartData | null {

        const proteinKcal = proteinGrams * this.KCAL_PER_PROTEIN;
        const carbsKcal = carbsGrams * this.KCAL_PER_CARB;
        const fatKcal = fatGrams * this.KCAL_PER_FAT;

        const totalKcal = proteinKcal + carbsKcal + fatKcal;

        if (totalKcal === 0) {
            return null;
        }

        return {
            caloriesData: [
                Math.round(proteinKcal),
                Math.round(carbsKcal),
                Math.round(fatKcal)
            ],
            totalCalories: Math.round(totalKcal)
        };
    }
}