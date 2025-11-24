import { CanActivateFn, Router } from '@angular/router';
import { inject, signal } from '@angular/core';
import { SnackBarService } from '../services/snackbar';
import { Account } from '../services/account';

export const AuthGuard: CanActivateFn = () => {
    const router = inject(Router);
    const snackBar = inject(SnackBarService)
    const accountService = inject(Account)

    if (accountService.currentUser())
        return true


    router.navigate(['/login']);
    snackBar.error("Trebuie să fii logat pentru a accesa această pagină!");

    return false;
};
