import { HttpClient } from "@angular/common/http";
import { Injectable, signal, computed } from "@angular/core"; // 1. Importă 'computed'
import { GeneratedMealDto } from "../../shared/models/Menu";

@Injectable({
    providedIn: 'root'
})
export class GeneratorService {
    baseUrl = 'https://localhost:7000/api/menu/'
    private readonly STORAGE_KEY = 'generatedMenu';
    constructor(private http: HttpClient) { }

    public menuSource = signal<GeneratedMealDto[] | null>(null);


    public totalTargetKcal = computed(() => {
        const menu = this.menuSource();
        if (!menu) return 0;
        const total = menu.reduce((sum, meal) => sum + meal.targetKcal, 0);
        return Math.round(total);
    });

    public totalActualKcal = computed(() => {
        const menu = this.menuSource();
        if (!menu) return 0;
        const total = menu.reduce((sum, meal) => sum + meal.actualKcal, 0);
        return Math.round(total);
    });

    public totalMacros = computed(() => {
        const menu = this.menuSource();
        let p = 0, c = 0, f = 0;

        if (menu) {
            for (const meal of menu) {
                for (const recipe of meal.recipes) {
                    p += recipe.protein;
                    c += recipe.carbs;
                    f += recipe.fat;
                }
            }
        }

        return {
            p: Math.round(p),
            c: Math.round(c),
            f: Math.round(f)
        };
    });
    setMenu(menu: GeneratedMealDto[]) {
        this.menuSource.set(menu);
        localStorage.setItem(this.STORAGE_KEY, JSON.stringify(menu))
    }


    generateMenu() {
        const options = { withCredentials: true };
        return this.http.get<GeneratedMealDto[]>(this.baseUrl + 'generate', options);
    }

    removeMenu() {
        this.menuSource.set(null);
        localStorage.removeItem(this.STORAGE_KEY);
    }
}