import { Component, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/auth/auth.service';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login implements OnInit {
  email = '';
  password = '';
  errorMessage = signal('');

  constructor(
    private authService: AuthService,
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.redirectLoggedInUser();
  }

  login(): void {
    this.errorMessage.set('');

    this.authService
      .login({
        email: this.email,
        password: this.password,
      })
      .subscribe({
        next: (response) => {
          this.redirectAfterLogin(response.mustChangePassword);
        },
        error: () => {
          this.errorMessage.set('Invalid email or password.');
        },
      });
  }

  private redirectLoggedInUser(): void {
    const user = this.authService.currentUser();

    if (!user) {
      return;
    }

    this.redirectAfterLogin(user.mustChangePassword);
  }

  private redirectAfterLogin(mustChangePassword: boolean): void {
    if (mustChangePassword) {
      this.router.navigate(['/change-password']);
      return;
    }

    this.router.navigate(['/announcements']);
  }
}
