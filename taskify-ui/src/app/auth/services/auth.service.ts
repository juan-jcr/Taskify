import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { LoginRequest, LoginResponse } from "../interface/auth.interface";

@Injectable({providedIn: 'root'})
export class AuthService {
	private apiUrl = 'http://localhost:5146/api/auth';
	private htt = inject(HttpClient);

	login(credentials: LoginRequest){
		return this.htt.post<LoginResponse>(`${this.apiUrl}/login`, credentials);
	}

}