import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { Navbar } from './layout/navbar/navbar';
import { AuthService } from './core/auth/auth.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Navbar],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App {
  constructor(
    public authService: AuthService,
    public router: Router,
  ) {}

  shouldShowNavbar(): boolean {
    return this.authService.currentUser() !== null && this.router.url !== '/login';
  }
}
