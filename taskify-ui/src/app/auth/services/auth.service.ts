import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { LoginRequest} from "../interface/auth.interface";
import { catchError, map, Observable, of } from "rxjs";

@Injectable({providedIn: 'root'})
export class AuthService {
	private apiUrl = 'http://localhost:5146/api/auth';
	private http = inject(HttpClient);

	login(credentials: LoginRequest){
		return this.http.post(`${this.apiUrl}/login`, credentials, {withCredentials: true});
	}

	isAuthenticated(): Observable<boolean> {
    return this.http.get(`${this.apiUrl}/user-profile`, { withCredentials: true }).pipe(
      map(() => true),
      catchError(() => of(false))
    );
  }

}