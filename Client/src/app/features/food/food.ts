import { ChangeDetectorRef, Component, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { FoodService } from '../../core/services/foodService';
import { Food } from '../../shared/models/Food';
import { SnackBarService } from '../../core/services/snackbar';
import { NutritionChartService } from '../../core/services/nutritionChartServic';
import { PieChart } from '../../shared/components/pie-chart/pie-chart';

@Component({
  selector: 'app-food',
  imports: [PieChart, RouterLink],
  templateUrl: './food.html',
  styleUrl: './food.css',
})
export class FoodComponent implements OnInit {

  public constructor(private foodSerivce: FoodService, private snackBar: SnackBarService, private router: Router,
    public nutritionChartService: NutritionChartService, private route: ActivatedRoute) { }
  public foodItem = signal<Food | null>(null);

  ngOnInit(): void {
    this.getFoodById()
  }

  getFoodById() {
    const idString = this.route.snapshot.paramMap.get('id');
    if (idString) {
      const foodId = +idString;
      this.foodSerivce.getFoodById(foodId).subscribe({
        next: (result) => {
          this.foodItem.set(result)
          console.log(this.foodItem)
        },
        error: (err) => {
          this.snackBar.error('Rețeta nu a fost găsită');
          this.router.navigateByUrl('/menu');
          console.log(err)
        }
      })
    } else {
      this.snackBar.error('ID-ul rețetei lipsește.');
      this.router.navigateByUrl('/menu');
    }
  }

  getPieChartForFood() {
    const food = this.foodItem();
    if (!food) {
      return null;
    }
    const p = food.protein;
    const f = food.fat;
    const c = food.carbs;
    const data = this.nutritionChartService.calculateMacroSplit(p, c, f);
    return data ? data.caloriesData : null;
  }

}
