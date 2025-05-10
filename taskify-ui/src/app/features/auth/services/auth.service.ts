import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { LoginRequest } from '../interface/auth.interface';
import { catchError, map, Observable, of, throwError } from 'rxjs';
import { RegisterRequest } from '../interface/register.interface';
import {environment} from '../../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private http = inject(HttpClient);

  login(credentials: LoginRequest) {
    return this.http.post(`${environment.apiUrl}/auth/login`, credentials, {
      withCredentials: true,
    }).pipe(
      catchError(err => {
        if(err.status === 401){
          return throwError(() => "Credenciales incorrectas, intente de nuevo.");
        }
        return throwError(() => "Error desconocido");
      })
    );
  }

  registerUser(credentials: RegisterRequest) {
    return this.http.post(`${environment.apiUrl}/auth/register`, credentials).pipe(
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
      .get(`${environment.apiUrl}/auth/user-profile`, { withCredentials: true })
      .pipe(
        map(() => true),
        catchError(() => of(false))
      );
  }
}
