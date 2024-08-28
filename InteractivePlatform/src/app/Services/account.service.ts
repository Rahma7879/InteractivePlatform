import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Inject, Injectable, PLATFORM_ID } from '@angular/core';

import { BehaviorSubject, catchError, of, tap } from 'rxjs';
import { UserLogin } from '../Models/user-login';
import * as jwtdecode from 'jwt-decode';
import { Router } from '@angular/router';
import { isPlatformBrowser } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  r: { isAdmin: boolean; name: string; UserId: number } = { isAdmin: true,  name: "", UserId: 0 };
  isAuthenticated = false;
  baseUrl = "https://localhost:7061/api/Account";

  constructor(
    public http: HttpClient,
    public router: Router,
    @Inject(PLATFORM_ID) private platformId: Object 
  ) {
    this.checkToken();
  }

  private checkToken(): void {
    if (isPlatformBrowser(this.platformId)) {
      const token = localStorage.getItem("token");
      if (token) {
        try {
          this.isAuthenticated = true;
          this.r = jwtdecode.jwtDecode<{ isAdmin: boolean; isCustomer: boolean; name: string; UserId: number }>(token);
          console.log(this.r.isAdmin);
         
          console.log(this.r.name);
          console.log(this.r.UserId);
        } catch (error) {
          console.error("Failed to decode token", error);
          this.isAuthenticated = false;
        }
      } else {
        this.isAuthenticated = false;
      }
    }
  }

  login(user: UserLogin) {
    const headers = { 'Content-Type': 'application/json' };
    console.log('Attempting to login with user:', user); 
    console.log('user:', user.email);
    this.http.post<string>(`${this.baseUrl}/Login`, user, { headers, responseType: 'text' as 'json' })
      .subscribe({
        next: (token) => {
          console.log('Token received:', token); 
          
          
          this.isAuthenticated = true;
          localStorage.setItem("token", token);

          try {
              
              this.r = jwtdecode.jwtDecode<{ isAdmin: boolean; name: string; UserId: number }>(token);
              console.log('Decoded token:', this.r); 
              
              
              if (this.r.isAdmin) {
                console.log('User is admin, navigating to /Finance');
                this.router.navigateByUrl("/Finance");
              } else {
                
                this.showError("Access denied: You do not have permission to access the finance page.");
              }
          } catch (decodeError) {
              
              console.error("Failed to decode token", decodeError);
              this.showError("Invalid token received. Please try again.");
          }
        },
        error: (err: HttpErrorResponse) => {
        
          console.error("Login failed", err);
          this.showError("Login failed. Please check your credentials and try again.");
        }
      });
}


  
  
  logout() {
    if (isPlatformBrowser(this.platformId)) { // Ensure this code runs only in the browser
      this.isAuthenticated = false;
      localStorage.removeItem("token");
    }
  }

  private showError(message: string) {
    console.error(message);
    alert(message);
  }
}