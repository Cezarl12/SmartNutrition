import { inject, Injectable } from '@angular/core';
import { Account } from './account';
// 1. Importă operatorii necesari
import { lastValueFrom, of, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private accountService = inject(Account)

    init(): Observable<boolean | {}> {

        return this.accountService.getUserInfo().pipe(
            catchError((err) => {
                if (err.status !== 401) {
                    console.error('Eroare neașteptată la verificarea autentificării:', err);
                }
                return of(false);
            })
        );
    }
}