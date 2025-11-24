export type Ingredient = {
    id: number;
    name: string | null;
    imageUrl: string | null;
    isVegan: boolean;
    isVegetarian: boolean;
    kcalPer100g: number;
    proteinPer100g: number;
    carbsPer100g: number;
    fatPer100g: number;
}