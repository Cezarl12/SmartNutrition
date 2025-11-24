export type User = {
    email: string | null;
    firstName: string | null;
    lastName: string | null;
    age?: number | null;
    sex?: number | null;
    height?: number | null;
    weight?: number | null;
    activityLevel?: number | null;
    goal?: number | null;
    preferences?: number | null;
    numberOfMeals?: number | null;
    target?: Target | null;
}

export interface Target {
    dailyKcal: number;
    dailyProtein: number;
    dailyCarbs: number;
    dailyFat: number;
}