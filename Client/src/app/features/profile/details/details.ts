import { NgClass } from '@angular/common';
import { Component, inject, OnInit, Signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Account } from '../../../core/services/account';
import { RouterLink } from '@angular/router';
import { SnackBarService } from '../../../core/services/snackbar';
import { User } from '../../../shared/models/User';

const preferenceMap = [
  { id: 1, name: 'Standard' },
  { id: 2, name: 'Vegetarian' },
  { id: 3, name: 'Vegan' },
  { id: 4, name: 'Keto' }
];

@Component({
  selector: 'app-details',
  imports: [ReactiveFormsModule, NgClass, RouterLink],
  templateUrl: './details.html',
  styleUrl: './details.css',
})
export class Details implements OnInit {

  public preferenceOptions = preferenceMap;
  public mealOptions: number[] = [1, 2, 3, 4, 5];

  public account: Signal<User | null>;
  private fb = inject(FormBuilder)
  public constructor(private accountService: Account, private snackBar: SnackBarService) {
    this.account = this.accountService.currentUser
  }


  ngOnInit(): void {
    const user = this.account();

    if (user) {
      this.profileForm.patchValue({
        firstName: user.firstName,
        lastName: user.lastName,
        email: user.email,
        preferences: user.preferences,
        numberOfMeals: user.numberOfMeals
      });
    }
  }

  profileForm = this.fb.group({
    firstName: ['', [Validators.required]],
    lastName: ['', [Validators.required]],
    email: ['', [Validators.email, Validators.required]],
    preferences: [null as number | null],
    numberOfMeals: [null as number | null]
  })

  onSubmit() {
    const currentUser = this.account();
    const dataToSave: User = {
      ...currentUser,
      ...this.profileForm.getRawValue()
    };
    this.accountService.updateProfile(dataToSave).subscribe({
      next: () => {
        this.snackBar.success('Profilul a fost actualizat cu succes!');
        this.profileForm.reset(this.profileForm.getRawValue());
      },
      error: (err) => {
        this.snackBar.error('A apărut o eroare la salvare. Te rugăm să încerci din nou.');
        console.error(err);
      }
    })
  }

  get email() {
    return this.profileForm.get('email');
  }

  get firstName() {
    return this.profileForm.get('firstName');
  }

  get lastName() {
    return this.profileForm.get('lastName');
  }
  get preferences() {
    return this.profileForm.get('preferences');
  }
  get numberOfMeals() {
    return this.profileForm.get('numberOfMeals');
  }


}
