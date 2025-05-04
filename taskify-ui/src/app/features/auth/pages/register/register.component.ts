import { Component, inject, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RegisterRequest } from '../../interface/register.interface';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  imports: [RouterLink, ReactiveFormsModule, CommonModule],
  templateUrl: './register.component.html',
})
export class RegisterComponent {
  private readonly authService = inject(AuthService);
  private readonly formBuilder = inject(FormBuilder);

  registerForm: FormGroup = this.formBuilder.group({
    name: ['', [Validators.required]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]]
  })

  successMessage = signal<string>('');
  errorMessage = signal<string>('');
  isLoading = signal<boolean>(false);

  registered = false;

  register(){
    this.registered = true;

    if(this.registerForm.invalid){
      return;
    }

    this.errorMessage.set('');
    this.successMessage.set('');

    if (this.isLoading()) return;
    this.isLoading.set(true);

    const credentials: RegisterRequest = this.registerForm.value;

    this.authService.registerUser(credentials).subscribe({
      next: () => {
        this.successMessage.set('¡Registro exitoso!');
        this.registerForm.reset();
      },
      error : (error) => {
        this.errorMessage.set(error);
        this.isLoading.set(false);
      },
      complete: () => {this.isLoading.set(false);}
    })
  }

  get name (){
    return this.registerForm.get('name');
  }

  get email() {
    return this.registerForm.get('email');
  }

  get password() {
    return this.registerForm.get('password');
  }
}
