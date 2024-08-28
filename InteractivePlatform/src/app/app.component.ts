import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { UserLoginComponent } from "./Page/user-login/user-login.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, UserLoginComponent,RouterLink],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'InteractivePlatform';
}
