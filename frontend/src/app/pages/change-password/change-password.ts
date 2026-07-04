import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/auth/auth.service';

@Component({
  selector: 'app-change-password',
  imports: [FormsModule],
  templateUrl: './change-password.html',
})
export class ChangePassword {
  currentPassword = '';
  newPassword = '';
  errorMessage = '';

  constructor(
    private authService: AuthService,
    private router: Router,
  ) {}

  changePassword(): void {
    this.errorMessage = '';

    this.authService
      .changePassword({
        currentPassword: this.currentPassword,
        newPassword: this.newPassword,
      })
      .subscribe({
        next: () => this.router.navigate(['/home']),
        error: () => {
          this.errorMessage = 'Could not change password.';
        },
      });
  }
}
