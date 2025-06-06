import { inject, Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../../features/auth/services/auth.service';
import { Observable, map } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  private auth = inject(AuthService);
  private router = inject(Router);

  canActivate(): Observable<boolean> {
    return this.auth.isAuthenticated().pipe(
      map(authenticated => {
        if (!authenticated) {
          this.router.navigate(['auth/login']);
          return false;
        }
        return true;
      })
    );
  }
}
