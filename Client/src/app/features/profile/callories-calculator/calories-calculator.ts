import { Component, inject, OnInit, Signal } from '@angular/core';
import { User } from '../../../shared/models/User';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Account } from '../../../core/services/account';
import { Router } from '@angular/router';
import { SnackBarService } from '../../../core/services/snackbar';
import { NgClass } from '@angular/common';

const sexMap = [
  { id: 1, name: 'Female' },
  { id: 2, name: 'Male' },
];

const activityMap = [
  { id: 1, name: 'Sedentary' },
  { id: 2, name: 'Light' },
  { id: 3, name: 'Moderate' },
  { id: 4, name: 'Active' }
];
const goalMap = [
  { id: 1, name: 'Lose' },
  { id: 2, name: 'Maintain' },
  { id: 3, name: 'Gain' }
];

@Component({
  selector: 'app-callories-calculator',
  imports: [ReactiveFormsModule, NgClass],
  templateUrl: './calories-calculator.html',
  styleUrl: './calories-calculator.css',
})
export class CaloriesCalculator implements OnInit {
  public account: Signal<User | null>;
  private fb = inject(FormBuilder)

  public sexOptions = sexMap;
  public activityOptions = activityMap;
  public goalOptions = goalMap;

  public constructor(private accountService: Account, private router: Router, private snackBar: SnackBarService) {
    this.account = this.accountService.currentUser
  }
  ngOnInit(): void {
    const user = this.account();
    if (user) {
      this.profileForm.patchValue({
        age: user.age ? user.age : null,
        sex: user.sex ? user.sex : null,
        height: user.height ? user.height : null,
        weight: user.weight ? user.weight : null,
        activityLevel: user.activityLevel ? user.activityLevel : null,
        goal: user.goal ? user.goal : null
      });
    }
  }

  profileForm = this.fb.group({
    age: [null as number | null, [Validators.required, Validators.min(8), Validators.max(150)]],
    sex: [null as number | null, [Validators.required]],
    height: [null as number | null, [Validators.required, Validators.min(100)]],
    weight: [null as number | null, [Validators.required, Validators.min(30)]],
    activityLevel: [null as number | null, [Validators.required]],
    goal: [null as number | null, [Validators.required]],
  });

  onSubmit() {
    const currentUser = this.account();

    if (!currentUser) {
      return
    }
    const dataToSave: User = {
      ...currentUser,
      ...this.profileForm.getRawValue()
    }
    this.accountService.updateProfile(dataToSave).subscribe({
      next: () => {
        this.snackBar.success('Targetul tău caloric a fost calculat și salvat!');
        this.profileForm.reset(this.profileForm.getRawValue());
        this.router.navigateByUrl('/profile/details')
      },
      error: (err) => {
        this.snackBar.error('A apărut o eroare la calculare. Te rugăm să încerci din nou.');
        console.error(err);
      }
    })
  }


  get age() { return this.profileForm.get('age'); }
  get sex() { return this.profileForm.get('sex'); }
  get height() { return this.profileForm.get('height'); }
  get weight() { return this.profileForm.get('weight'); }
  get activityLevel() { return this.profileForm.get('activityLevel'); }
  get goal() { return this.profileForm.get('goal'); }
}
