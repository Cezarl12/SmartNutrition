import { Food } from "./Food";


export interface GeneratedMealDto {
    mealName: string;
    targetKcal: number;
    actualKcal: number;
    recipes: Food[];
}