import { Routes } from '@angular/router';
import LoginComponent from './auth/pages/login/login.component';
import { RegisterComponent } from './auth/pages/register/register.component';
import { AuthGuard } from './shared/guard/auth.guard';
import { AboutComponent } from './about/about/about.component';
import { PublicGuard } from './shared/guard/public.guard';


export const routes: Routes = [
  {
    path: '', component: AboutComponent
  },
  {
    path: 'auth',
    loadComponent: () => import('./auth/layout/auth.layout.component'),
    children: [
      {path: 'login', component: LoginComponent, canActivate : [PublicGuard]},
      {path: 'register', component: RegisterComponent}
    ]
  },
  {
    path: 'home', loadComponent: () => import('./tasks/pages/tasks/tasks.component'), canActivate: [AuthGuard],
  },

];
