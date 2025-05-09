import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { LoginRequest } from '../interface/auth.interface';
import { catchError, map, Observable, of, throwError } from 'rxjs';
import { RegisterRequest } from '../interface/register.interface';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = 'http://localhost:5000/api/auth';
  private http = inject(HttpClient);

  login(credentials: LoginRequest) {
    return this.http.post(`${this.apiUrl}/login`, credentials, {
      withCredentials: true,
    });
  }

  registerUser(credentials: RegisterRequest) {
    return this.http.post(`${this.apiUrl}/register`, credentials).pipe(
      catchError(error => {
        //Email already registered
        if (error.status === 409) {
          return throwError(() => 'Este email ya está registrado');
        }
        return throwError(() => 'Error desconocido durante el registro');
      })
    );
  }

  isAuthenticated(): Observable<boolean> {
    return this.http
      .get(`${this.apiUrl}/user-profile`, { withCredentials: true })
      .pipe(
        map(() => true),
        catchError(() => of(false))
      );
  }
}
