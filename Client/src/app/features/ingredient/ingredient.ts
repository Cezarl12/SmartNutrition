import { Component, inject, OnInit, signal } from '@angular/core';
import { FoodService } from '../../core/services/foodService';
import { SnackBarService } from '../../core/services/snackbar';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { NutritionChartService } from '../../core/services/nutritionChartServic';
import { IngredintService } from '../../core/services/ingredintService';
import { Ingredient } from '../../shared/models/Ingredient';
import { Food } from '../../shared/models/Food';
import { PieChart } from '../../shared/components/pie-chart/pie-chart';

@Component({
  selector: 'app-ingredient',
  imports: [PieChart, RouterLink],
  templateUrl: './ingredient.html',
  styleUrl: './ingredient.css',
})
export class IngredientComponent implements OnInit {

  public constructor(private foodSerivce: FoodService, private ingredientService: IngredintService, private snackBar: SnackBarService,
    private router: Router, public nutritionChartService: NutritionChartService, private route: ActivatedRoute) { }
  public ingredientItem = signal<Ingredient | null>(null);
  public foods = signal<Food[] | null>(null);

  ngOnInit(): void {
    this.getIngredientById();
  }

  getIngredientById() {
    const idString = this.route.snapshot.paramMap.get('id');
    if (idString) {
      const foodId = +idString;
      this.ingredientService.getIngredientById(foodId).subscribe({
        next: (result) => {
          console.log(result)
          this.ingredientItem.set(result)
          this.foodSerivce.getFoodsByIngrdientId(result.id).subscribe({
            next: (result) => {
              this.foods.set(result)
            }
          })
        },
        error: (err) => {
          this.snackBar.error('Ingredientul nu a fost găsit' + err);
          this.router.navigateByUrl('/menu');
          console.log(err)
        }
      })
    } else {
      this.snackBar.error('ID-ul ingredientului lipsește.');
      this.router.navigateByUrl('/menu');
    }
  }

  getPieChartForIngredient() {
    const ingredient = this.ingredientItem();
    if (!ingredient) {
      return null;
    }
    const p = ingredient.proteinPer100g;
    const f = ingredient.fatPer100g;
    const c = ingredient.fatPer100g;
    const data = this.nutritionChartService.calculateMacroSplit(p, c, f);
    return data ? data.caloriesData : null;
  }

}
