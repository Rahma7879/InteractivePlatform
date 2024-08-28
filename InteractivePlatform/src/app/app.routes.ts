import { Routes } from '@angular/router';
import { FinanceComponent } from './Page/finance/finance.component';
import { UserLoginComponent } from './Page/user-login/user-login.component';
import { authGuard } from './auth.guard';

export const routes: Routes = [
  { path: 'login', component: UserLoginComponent },
  { path: 'Finance', component: FinanceComponent, canActivate: [authGuard] }, 
  { path: '', redirectTo: '/login', pathMatch: 'full' } 
];
