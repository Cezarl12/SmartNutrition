import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function passwordValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
        const value = control.value || '';
        const errors: ValidationErrors = {};

        if (value.length < 6) {
            errors['PasswordTooShort'] = ['Parola trebuie să aibă cel puțin 6 caractere.'];
        }
        if (!/[a-z]/.test(value)) {
            errors['PasswordRequiresLower'] = ['Parola trebuie să conțină cel puțin o literă mică (a–z).'];
        }

        if (!/[A-Z]/.test(value)) {
            errors['PasswordRequiresUpper'] = ['Parola trebuie să conțină cel puțin o literă mare (A–Z).'];
        }

        if (!/[0-9]/.test(value)) {
            errors['PasswordRequiresDigit'] = ['Parola trebuie să conțină cel puțin o cifră (0–9).'];
        }

        if (!/[^a-zA-Z0-9]/.test(value)) {
            errors['PasswordRequiresNonAlphanumeric'] = ['Parola trebuie să conțină cel puțin un caracter special (ex: !, @, #, $, %, etc.).'];
        }

        return Object.keys(errors).length ? errors : null;
    };
}
