export type Food = {
    id: number;
    name: string | null;
    description: string | null;
    imageUrl: string | null;
    standardServingSizeInGrams: number;
    isVegan: boolean;
    isVegetarian: boolean;
    kcal: number;
    protein: number;
    carbs: number;
    fat: number;
    ingredients: IngredientDto[];
}

export interface IngredientDto {
    id: number;
    name: string | null;
    quantityInGrams: number;
    imageUrl: string;
}
