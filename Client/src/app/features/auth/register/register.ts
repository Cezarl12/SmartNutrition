import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Account } from '../../../core/services/account';
import { Router } from '@angular/router';
import { NgClass } from '@angular/common';
import { passwordsMatchValidator } from '../../../shared/validators/PasswordMatchValidator';
import { SnackBarService } from '../../../core/services/snackbar';
import { passwordValidator } from '../../../shared/validators/PasswordValidator';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, NgClass],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
  private fb = inject(FormBuilder)
  constructor(private accountService: Account, private router: Router, private snackBar: SnackBarService) { }
  showPassword = false;
  showConfirmPassword = false;

  registerForm = this.fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, passwordValidator()]],
    confirmpassword: ['', Validators.required]
  }, {
    validators: passwordsMatchValidator()
  })

  onSubmit() {
    const { confirmpassword, ...registerData } = this.registerForm.value;
    this.accountService.register(registerData).subscribe({
      next: () => {
        this.router.navigateByUrl('/login')
        this.snackBar.success('Inregistrarea a avut loc cu succes! Acum te poti loga.')
      },
      error: (errors) => {
        console.log(errors)
        this.snackBar.error('Eroare la inregistrare!')
      }
    })
  }

  get email() {
    return this.registerForm.get('email');
  }

  get password() {
    return this.registerForm.get('password');
  }

  get firstName() {
    return this.registerForm.get('firstName');
  }

  get lastName() {
    return this.registerForm.get('lastName');
  }

  get confirmpassword() {
    return this.registerForm.get('confirmpassword');
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }

  toggleConfirmPasswordVisibility() {
    this.showConfirmPassword = !this.showConfirmPassword;
  }
}
