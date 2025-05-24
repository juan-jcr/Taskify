import { inject, Injectable } from '@angular/core';
import {CanActivate, Router, UrlTree} from '@angular/router';
import { AuthService } from '../../features/auth/services/auth.service';
import { Observable, tap } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  private auth = inject(AuthService);
  private router = inject(Router);

  canActivate(): Observable<boolean> {
    return this.auth.isAuthenticated().pipe(
      tap((authenticated) => {
        if (!authenticated) this.router.navigate(['auth/login']);
      })
    );
  }
}
