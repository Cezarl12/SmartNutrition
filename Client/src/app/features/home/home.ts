import { NgClass } from '@angular/common';
import { Component, Signal } from '@angular/core';
import { User } from '../../shared/models/User';
import { Account } from '../../core/services/account';
import { SnackBarService } from '../../core/services/snackbar';
import { Router, RouterLink } from '@angular/router';
import { GeneratorService } from '../../core/services/generatorService';
import { GeneratedMealDto } from '../../shared/models/Menu';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-home',
  imports: [NgClass, RouterLink, ReactiveFormsModule, FormsModule],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {

  selectedDiet: string | null = null;
  selectedDietId: number | null = null;
  public account: Signal<User | null>;

  constructor(private accountService: Account, private snackBar: SnackBarService, private router: Router,
    private generatorService: GeneratorService) {
    this.account = this.accountService.currentUser;
  }

  generate() {
    if (!this.account()) {
      this.snackBar.error('Trebuie să fii autentificat pentru a genera un plan.')
      this.router.navigateByUrl('/account/login')
    }
    if (this.account()?.target?.dailyKcal === 0) {
      this.snackBar.error('Calculeaza-ti necesarul caloric inainte!')
      return
    }
    if (!this.selectedDietId) {
      this.snackBar.error('Selecteaza un tip de dieta inainte')
      return
    }
    if (!this.selectedMeals) {
      this.snackBar.error('Selecteaza  numarul de mese inainte')
      return
    }
    const currentUser = this.account();
    if (!currentUser) {
      return
    }
    const dataToSave: User = {
      ...currentUser,
      preferences: this.selectedDietId,
      numberOfMeals: this.selectedMeals,
    };

    this.accountService.updateProfile(dataToSave).subscribe({
      next: () => {
        this.generatorService.generateMenu().subscribe({
          next: (result: GeneratedMealDto[]) => {
            this.generatorService.setMenu(result)
            this.snackBar.success('Meniul tău a fost generat!');
            this.router.navigateByUrl('/menu')
          },
          error: (err) => {
            this.snackBar.error("Eroare la generare!");
            console.error(err);
          }
        });
      },
      error: (err) => {
        this.snackBar.error("Eroare la salvarea preferințelor!");
        console.error(err);
      }
    });
  }

  selectDiet(diet: string): void {
    this.selectedDiet = diet;

    switch (diet) {
      case 'Everything':
        this.selectedDietId = 1;
        break;
      case 'Vegetarian':
        this.selectedDietId = 2;
        break;
      case 'Vegan':
        this.selectedDietId = 3;
        break;
      case 'Keto':
        this.selectedDietId = 4;
        break;
      default:
        this.selectedDietId = null;
    }
  }
  public mealOptions: number[] = [1, 2, 3, 4, 5];

  public selectedMeals: number | null = null;

  onMealChange(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    const value = selectElement.value;
    this.selectedMeals = value === 'null' ? null : +value;
  }
}
