import { Component, OnInit, signal } from '@angular/core';
import { GeneratedMealDto } from '../../shared/models/Menu';
import { Food } from '../../shared/models/Food';
import { PieChart } from '../../shared/components/pie-chart/pie-chart';
import { NutritionChartService } from '../../core/services/nutritionChartServic';
import { GeneratorService } from '../../core/services/generatorService';
import { SnackBarService } from '../../core/services/snackbar';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { DailyLog, DailyLogDto, DailyLogMeal, DailyLogRecipe } from '../../shared/models/DailyLog';
import { DailyLogService } from '../../core/services/dailyLogService';
import { map, retry } from 'rxjs';

@Component({
  selector: 'app-menu',
  imports: [PieChart, RouterLink, FormsModule],
  templateUrl: './menu.html',
  styleUrl: './menu.css',
})
export class Menu implements OnInit {

  public menuSource = signal<GeneratedMealDto[] | null>(null);
  public quantities: number[] = [];

  public constructor(public nutritionChartService: NutritionChartService, public generatorService: GeneratorService,
    private snackBar: SnackBarService, private router: Router, private dailyLogService: DailyLogService) { }

  ngOnInit(): void {
    const savedMenu = localStorage.getItem('generatedMenu');

    if (savedMenu) {
      const menu: GeneratedMealDto[] = JSON.parse(savedMenu);
      this.generatorService.setMenu(menu);
      this.menuSource.set(this.generatorService.menuSource())

      this.quantities = menu.flatMap(meal => meal.recipes.map(r => r.standardServingSizeInGrams));

    } else {
      this.snackBar.error('Nu există un meniu generat. Te rugăm să generezi unul nou.');
      this.router.navigateByUrl('/home');
    }
  }

  generateOther() {
    this.generatorService.generateMenu().subscribe({
      next: (result: GeneratedMealDto[]) => {
        this.generatorService.setMenu(result)
        this.snackBar.success('Meniul tău a fost regenerat!');
      },
      error: (err) => {
        this.snackBar.error('Eroare la generare!')
      }
    })
  }

  public getPieChartDataForMeals(recipes: Food[]): number[] | null {
    const { p, c, f } = this.nutritionChartService.sumMacrosFromRecipes(recipes);
    const data = this.nutritionChartService.calculateMacroSplit(p, c, f);
    return data ? data.caloriesData : null;
  }

  public getPieChartDataTotal(): number[] | null {
    const p = this.generatorService.totalMacros().p;
    const c = this.generatorService.totalMacros().c;
    const f = this.generatorService.totalMacros().f;
    const data = this.nutritionChartService.calculateMacroSplit(p, c, f);
    return data ? data.caloriesData : null;
  }

  getRecipeIndex(mealIndex: number, recipeIndex: number): number {
    let index = 0;
    const menu = this.menuSource();
    if (!menu) return 0;
    for (let i = 0; i < mealIndex; i++) {
      index += menu[i].recipes.length;
    }
    return index + recipeIndex;
  }

  saveDaily() {
    const menu = this.menuSource();
    if (!menu) {
      return;
    }
    let flatIndex = 0;
    const mealDtos: DailyLogMeal[] = [];
    for (const meal of menu) {
      const recipeDtos: DailyLogRecipe[] = [];
      for (const recipe of meal.recipes) {
        if (flatIndex < this.quantities.length) {
          recipeDtos.push({
            foodId: recipe.id,
            name: recipe.name,
            quantityConsumedInGrams: this.quantities[flatIndex],
            imageUrl: recipe.imageUrl
          })
          flatIndex++;
        }
      }
      mealDtos.push({ mealName: meal.mealName, recipes: recipeDtos, totalCalories: meal.actualKcal });
      console.log(recipeDtos)
    }
    const dailyLogDto: DailyLogDto = {
      date: new Date().toISOString(),
      meals: mealDtos,
    };
    console.log(dailyLogDto)

    this.dailyLogService.saveDailyLog(dailyLogDto).subscribe({
      next: (savedLog) => {
        this.snackBar.success('Meniul a fost salvat cu succes!');
        this.router.navigateByUrl('/dailylogs');
        this.generatorService.removeMenu();
      },
      error: (err) => {
        if (err.status === 400) {
          const message = 'Ai deja un jurnal înregistrat pentru această zi.';
          this.snackBar.error(message);
          return;
        }
      }
    });
  }
}