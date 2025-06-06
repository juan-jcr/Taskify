import { TestBed } from '@angular/core/testing';
import { HttpErrorResponse, HttpEvent, HttpHandler, HttpRequest, HttpResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import {  of, throwError } from 'rxjs';
import { ErrorInterceptor } from './error.interceptor';

describe('ErrorInterceptor', () => {
  let interceptor: ErrorInterceptor;
  let mockMessageService: jasmine.SpyObj<MessageService>;
  let mockRouter: jasmine.SpyObj<Router>;
  let mockHttpHandler: jasmine.SpyObj<HttpHandler>;

  beforeEach(() => {
    mockMessageService = jasmine.createSpyObj('MessageService', ['add']);
    mockRouter = jasmine.createSpyObj('Router', ['navigate']);
    mockHttpHandler = jasmine.createSpyObj('HttpHandler', ['handle']);

    TestBed.configureTestingModule({
      providers: [
        ErrorInterceptor,
        { provide: MessageService, useValue: mockMessageService },
        { provide: Router, useValue: mockRouter }
      ]
    });

    interceptor = TestBed.inject(ErrorInterceptor);
  });

  it('debería ser creado', () => {
    expect(interceptor).toBeTruthy();
  });

  describe('intercept', () => {
    it('debería pasar por solicitudes exitosas', (done) => {
      const mockRequest = new HttpRequest('GET', '/test');
      const mockResponse = new HttpResponse({ status: 200 });

      mockHttpHandler.handle.and.returnValue(of(mockResponse));

      interceptor.intercept(mockRequest, mockHttpHandler).subscribe({
        next: (event: HttpEvent<unknown>) => {
          expect(event).toBe(mockResponse);
          done();
        },
        error: () => fail('should not error')
      });
    });

    it('debería manejar HttpErrorResponse y mostrar un mensaje de error', (done) => {
      const mockRequest = new HttpRequest('GET', '/test');
      const mockError = new HttpErrorResponse({
        status: 404,
        statusText: 'Not Found'
      });

      mockHttpHandler.handle.and.returnValue(throwError(() => mockError));

      interceptor.intercept(mockRequest, mockHttpHandler).subscribe({
        next: () => fail('debería haber dado un error'),
        error: (error) => {
          expect(error.message).toBe('Recurso no encontrado');
          expect(mockMessageService.add).toHaveBeenCalledWith({
            severity: 'error',
            summary: 'Error',
            detail: 'Recurso no encontrado',
            life: 5000,
            closable: true
          });
          done();
        }
      });
    });
  });

  describe('getErrorMessage', () => {
    it('debería devolver un mensaje de error del lado del cliente', () => {
      const mockError = new HttpErrorResponse({
        error: new ErrorEvent('Client error', { message: 'Test client error' })
      });
      const result = interceptor['getErrorMessage'](mockError);
      expect(result).toBe('Error: Test client error');
    });

    it('debería retornar el error 400 con errores de validación', () => {
      const mockError = new HttpErrorResponse({
        status: 400,
        error: { errors: { field1: ['Error 1'], field2: ['Error 2'] } }
      });
      const result = interceptor['getErrorMessage'](mockError);
      expect(result).toBe('Error 1\nError 2');
    });

    it('debería devolver un error 400 con un mensaje', () => {
      const mockError = new HttpErrorResponse({
        status: 400,
        error: { message: 'Bad request message' }
      });
      const result = interceptor['getErrorMessage'](mockError);
      expect(result).toBe('Bad request message');
    });

    it('debería devolver un mensaje de error 403', () => {
      const mockError = new HttpErrorResponse({ status: 403 });
      const result = interceptor['getErrorMessage'](mockError);
      expect(result).toBe('Acceso denegado');
    });

    it('debería devolver un mensaje de error 404', () => {
      const mockError = new HttpErrorResponse({ status: 404 });
      const result = interceptor['getErrorMessage'](mockError);
      expect(result).toBe('Recurso no encontrado');
    });

    it('debería devolver el mensaje 409: correo electrónico ya registrado', () => {
      const mockError = new HttpErrorResponse({
        status: 409,
        error: { error: 'Email already registered' }
      });
      const result = interceptor['getErrorMessage'](mockError);
      expect(result).toBe('El correo electrónico ya está registrado.');
    });

    it('debería devolver un mensaje de error genérico 409', () => {
      const mockError = new HttpErrorResponse({ status: 409 });
      const result = interceptor['getErrorMessage'](mockError);
      expect(result).toBe('Conflicto en la solicitud.');
    });

    it('debería devolver un mensaje de error 500', () => {
      const mockError = new HttpErrorResponse({ status: 500 });
      const result = interceptor['getErrorMessage'](mockError);
      expect(result).toBe('Error interno del servidor');
    });

    it('debería manejar el 401 con credenciales inválidas', () => {
      const mockError = new HttpErrorResponse({
        status: 401,
        error: { error: 'Invalid email or password' }
      });
      const result = interceptor['getErrorMessage'](mockError);
      expect(result).toBe('Creenciales incorrectas');
      expect(mockRouter.navigate).not.toHaveBeenCalled();
    });

    it('debería manejar el 401 y navegar a inicio de sesión', () => {
      const mockError = new HttpErrorResponse({ status: 401 });
      const result = interceptor['getErrorMessage'](mockError);
      expect(result).toBe('');
      expect(mockRouter.navigate).toHaveBeenCalledWith(['/auth/login']);
    });


  });

  describe('showError', () => {
    it('no debe mostrar mensaje cuando esté vacío', () => {
      interceptor['showError']('');
      expect(mockMessageService.add).not.toHaveBeenCalled();
    });

    it('debería mostrar un mensaje de error con los parámetros correctos', () => {
      const testMessage = 'Test error message';
      interceptor['showError'](testMessage);

      expect(mockMessageService.add).toHaveBeenCalledWith({
        severity: 'error',
        summary: 'Error',
        detail: testMessage,
        life: 5000,
        closable: true
      });
    });
  });
});
