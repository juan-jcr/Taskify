import { Routes } from '@angular/router';
import LoginComponent from './features/auth/pages/login/login.component';
import { RegisterComponent } from './features/auth/pages/register/register.component';
import { AuthGuard } from './shared/guard/auth.guard';
import { AboutComponent } from './features/about/about/about.component';
import { PublicGuard } from './shared/guard/public.guard';


export const routes: Routes = [
  {
    path: '', component: AboutComponent
  },
  {
    path: 'auth',
    loadComponent: () => import('./features/auth/layout/auth.layout.component'),
    children: [
      {path: 'login', component: LoginComponent, canActivate : [PublicGuard]},
      {path: 'register', component: RegisterComponent},
      {path: '**', redirectTo: '/'}   ]
  },
  {
    path: 'home', loadComponent: () => 
      import('./features/tasks/pages/tasks-page/tasks.component'), canActivate: [AuthGuard],
  },
  {
    path: '**', redirectTo: '/'
  }

];
