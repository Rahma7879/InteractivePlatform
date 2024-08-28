import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from './Services/account.service';


export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const router = inject(Router);

  
  if (accountService.isAuthenticated && accountService.r.isAdmin) {
    return true; 
  } else {
    router.navigate(['/login']); 
    return false; 
  }
};