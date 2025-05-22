import {Component, inject} from '@angular/core';
import {AuthService} from '../../../auth/services/auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-aside-profile',
  imports: [],
  templateUrl: './aside-profile.component.html'
})
export class AsideProfileComponent {
  private authService = inject(AuthService)
  private router = inject(Router)
  logout() {
    this.authService.logout().subscribe({
      next: () => {
        this.router.navigate(['/auth/login']);
      },
      error: () => {
        this.router.navigate(['/auth/login']);
      }
    })
  }
}
