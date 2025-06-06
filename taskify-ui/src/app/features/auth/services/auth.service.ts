import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { LoginRequest } from '../interface/auth.interface';
import {Observable } from 'rxjs';
import { RegisterRequest } from '../interface/register.interface';
import {environment} from '../../../../environments/environment';
import {UserRequest} from '../interface/user.interface';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private http = inject(HttpClient);
  private readonly apiUrl = `${environment.apiUrl}/auth`;

  login(credentials: LoginRequest) {
    return this.http.post(`${this.apiUrl}/login`, credentials, {
      withCredentials: true,
    });
  }

  register(credentials: RegisterRequest) {
    return this.http.post(`${this.apiUrl}/register`, credentials)
  }

  isAuthenticated(): Observable<boolean> {
    return this.http.get<boolean>(`${this.apiUrl}/isAuthenticated`, {
      withCredentials: true
    });
  }
  getProfileName() : Observable<UserRequest>{
    return this.http.get<UserRequest>(`${this.apiUrl}/user-profile`, { withCredentials: true })
  }

  logout() {
    return this.http.post(`${this.apiUrl}/logout`, {}, { withCredentials: true });
  }
}
