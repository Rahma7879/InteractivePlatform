import { Component } from '@angular/core';
import { UserLogin } from '../../Models/user-login';
import { Router } from '@angular/router';
import { AccountService } from '../../Services/account.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './user-login.component.html',
  styleUrl: './user-login.component.css'
})
export class UserLoginComponent {
  user:UserLogin=new UserLogin("","");

  constructor(public accountService:AccountService , public router:Router){}

  login() {
    this.accountService.login(this.user);
  }
}
