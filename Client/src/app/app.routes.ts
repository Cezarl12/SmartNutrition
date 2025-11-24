import { Routes } from '@angular/router';
import { Login } from './features/auth/login/login';
import { Register } from './features/auth/register/register';
import { SnackBarComponent } from './shared/components/snack-bar-component/snack-bar-component';
import { Home } from './features/home/home';
import { Profile } from './features/profile/profile';
import { Details } from './features/profile/details/details';
import { CaloriesCalculator } from './features/profile/callories-calculator/calories-calculator';
import { Menu } from './features/menu/menu';
import { IngredientComponent } from './features/ingredient/ingredient';
import { FoodComponent } from './features/food/food';
import { DailyLogs } from './features/daily-logs/daily-logs';
import { DailyItem } from './features/daily-logs/daily-item/daily-item';
import { AuthGuard } from './core/guards/auth-guard';
import { Report } from './features/report/report';


export const routes: Routes = [
    { path: '', component: Home },
    { path: 'menu', component: Menu, canActivate: [AuthGuard] },
    { path: 'home', component: Home },
    { path: 'login', component: Login },
    { path: 'register', component: Register },
    { path: 'test', component: SnackBarComponent },
    {
        path: 'profile', component: Profile, canActivate: [AuthGuard], children: [
            { path: '', redirectTo: 'details', pathMatch: 'full' },
            { path: 'details', component: Details },
            { path: 'calories-calculator', component: CaloriesCalculator }
        ]
    },
    { path: 'food/:id', component: FoodComponent },
    { path: 'ingredient/:id', component: IngredientComponent },
    { path: 'dailylogs', component: DailyLogs, canActivate: [AuthGuard] },
    { path: 'dailylogs/:id', component: DailyItem, canActivate: [AuthGuard] },
    { path: 'report', component: Report, canActivate: [AuthGuard] }
];
