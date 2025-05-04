import {Component, inject, signal} from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { LoginRequest } from '../../interface/auth.interface';

@Component({
  selector: 'app-login',
  imports: [RouterLink, ReactiveFormsModule],
  templateUrl: './login.component.html'
})

export default class LoginComponent {

  private readonly authService = inject(AuthService);
  private readonly formBuilder = inject(FormBuilder);
  private readonly route = inject(Router);

  loginForm : FormGroup = this.formBuilder.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required]]
  })

  isLoading = signal<boolean>(false);
  errorMessage : string | null = null;
  submitted = false;

  onSubmit(){

    this.submitted = true;
    if (this.loginForm.invalid) {
      return;
    }

    if (this.isLoading()) return;
    this.isLoading.set(true);

    const credentials: LoginRequest = this.loginForm.value

    this.authService.login(credentials).subscribe({
      next: () => {
        this.route.navigate(['/home'])
      },
      error: () => {
        this.errorMessage = 'Credenciales incorrectas, intente de nuevo.';
        this.isLoading.set(false);
      },
      complete: () => {
        this.isLoading.set(false);
      }
  });
  }

  get email() {
    return this.loginForm.get('email');
  }

  get password() {
    return this.loginForm.get('password');
  }
}
