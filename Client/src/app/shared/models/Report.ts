export type AverageSummaryDto = {
    averageKcalConsumed: number;
    averageProteinConsumed: number;
    averageCarbsConsumed: number;
    averageFatConsumed: number;

    targetKcal: number;
    targetProtein: number;
    targetCarbs: number;
    targetFat: number;

    calorieDeficitOrSurplus: number;
}

export type DailyDataDto = {
    date: string;
    totalKcal: number;
    totalProtein: number;
    totalCarbs: number;
    totalFat: number;
}