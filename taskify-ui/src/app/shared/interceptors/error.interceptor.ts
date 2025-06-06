import { inject, Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  private router = inject(Router);
  private messageService = inject(MessageService);

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        const message = this.getErrorMessage(error);
        this.showError(message);
        return throwError(() => new Error(message));
      })
    );
  }

  private getErrorMessage(error: HttpErrorResponse): string {
    if (error.error instanceof ErrorEvent) {
      return `Error: ${error.error.message}`;
    }

    switch (error.status) {
      case 400:
        return this.handleBadRequest(error);
      case 403:
        return 'Acceso denegado';
      case 404:
        return 'Recurso no encontrado';
      case 409:
        if (error.error?.error === 'Email already registered') {
          return 'El correo electrónico ya está registrado.';
        }
        return 'Conflicto en la solicitud.';
      case 500:
        return 'Error interno del servidor';
      case 401:
        if (error.error?.error === 'Invalid email or password') {
          return 'Creenciales incorrectas';
        }
        this.router.navigate(['/auth/login']);
        return '';
      default:
        return `Error ${error.status}: ${error.message}`;
    }
  }

  private handleBadRequest(error: HttpErrorResponse): string {
    if (error.error?.errors) {
      return Object.values(error.error.errors).join('\n');
    }
    return error.error?.message || 'Solicitud incorrecta';
  }

  private showError(message: string): void {
    if (!message) return;
    this.messageService.add({
      severity: 'error',
      summary: 'Error',
      detail: message,
      life: 5000,
      closable: true
    });
  }

}

