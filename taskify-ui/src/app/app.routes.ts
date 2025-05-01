import { Routes } from '@angular/router';
import LoginComponent from './auth/pages/login/login.component';

export const routes: Routes = [
  {
    path: 'auth',
    loadComponent: () => import('./auth/layout/auth.layout.component'),
    children: [
      {path: 'login', component: LoginComponent}
    ]
  },
];
