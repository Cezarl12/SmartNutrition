import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Account } from '../../../core/services/account';
import { Router } from '@angular/router';
import { NgClass } from '@angular/common';
import { SnackBarService } from '../../../core/services/snackbar';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, NgClass],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  showPassword = false;
  private fb = inject(FormBuilder)
  constructor(private accountService: Account, private router: Router, private snackBar: SnackBarService) { }

  loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required]]
  })

  get email() {
    return this.loginForm.get('email');
  }

  get password() {
    return this.loginForm.get('password');
  }

  onSubmit() {
    this.accountService.login(this.loginForm.value).subscribe({
      next: () => {
        this.snackBar.success("Logarea a avut loc cu succes!")
        this.accountService.getUserInfo().subscribe()
        this.router.navigateByUrl('/');
      },
      error: () => this.snackBar.error('Email sau parolă incorectă!')
    })
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }
}
