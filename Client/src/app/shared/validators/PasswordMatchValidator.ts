import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function passwordsMatchValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {

        const password = control.get('password');
        const confirmpassword = control.get('confirmpassword');

        if (!password || !confirmpassword) {
            return null;
        }

        if (confirmpassword.value === '') {
            return null;
        }

        return password.value === confirmpassword.value ? null : { passwordsDoNotMatch: true };
    };
}