import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { ChangePasswordRequest, LoginRequest, LoginResponse } from './auth.models';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly apiUrl = 'https://localhost:7166/api/auth';
  private readonly tokenKey = 'meridian_token';
  private readonly userKey = 'meridian_user';

  currentUser = signal<LoginResponse | null>(this.getStoredUser());

  constructor(private http: HttpClient) {}

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/login`, request).pipe(
      tap((response) => {
        localStorage.setItem(this.tokenKey, response.token);
        localStorage.setItem(this.userKey, JSON.stringify(response));
        this.currentUser.set(response);
      }),
    );
  }

  changePassword(request: ChangePasswordRequest): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/change-password`, request).pipe(
      tap(() => {
        const user = this.currentUser();

        if (!user) {
          return;
        }

        const updatedUser = { ...user, mustChangePassword: false };
        localStorage.setItem(this.userKey, JSON.stringify(updatedUser));
        this.currentUser.set(updatedUser);
      }),
    );
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.userKey);
    this.currentUser.set(null);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  isLoggedIn(): boolean {
    return this.getToken() !== null;
  }

  isHrOrAdmin(): boolean {
    const role = this.currentUser()?.role.toLowerCase();
    return role === 'hr' || role === 'admin';
  }

  private getStoredUser(): LoginResponse | null {
    const userJson = localStorage.getItem(this.userKey);

    if (!userJson) {
      return null;
    }

    return JSON.parse(userJson) as LoginResponse;
  }
}

