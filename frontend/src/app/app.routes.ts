import { Routes } from '@angular/router';
import { Calendar } from './pages/calendar/calendar';
import { Employees } from './pages/employees/employees';
import { Home } from './pages/home/home';
import { Login } from './pages/login/login';
import { NewEmployee } from './pages/new-employee/new-employee';
import { Profile } from './pages/profile/profile';
import { ChangePassword } from './pages/change-password/change-password';
import { authGuard, hrOrAdminGuard } from './core/auth/auth.guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  },
  {
    path: 'login',
    component: Login,
  },
  {
    path: 'home',
    component: Home,
    canActivate: [authGuard],
  },
  {
    path: 'calendar',
    component: Calendar,
    canActivate: [authGuard],
  },
  {
    path: 'profile',
    component: Profile,
    canActivate: [authGuard],
  },
  {
    path: 'change-password',
    component: ChangePassword,
    canActivate: [authGuard],
  },
  {
    path: 'new-employee',
    component: NewEmployee,
    canActivate: [authGuard, hrOrAdminGuard],
  },
  {
    path: 'employees',
    component: Employees,
    canActivate: [authGuard, hrOrAdminGuard],
  },
];
