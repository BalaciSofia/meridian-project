import { Routes } from '@angular/router';
import { Calendar } from './pages/calendar/calendar';
import { Employees } from './pages/employees/employees';
import { EmployeeDetails } from './pages/employee-details/employee-details';
import { Home } from './pages/home/home';
import { Login } from './pages/login/login';
import { NewEmployee } from './pages/new-employee/new-employee';
import { Profile } from './pages/profile/profile';
import { ChangePassword } from './pages/change-password/change-password';
import { authGuard, hrOrAdminGuard, passwordChangedGuard } from './core/auth/auth.guard';

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
    canActivate: [authGuard, passwordChangedGuard],
  },
  {
    path: 'calendar',
    component: Calendar,
    canActivate: [authGuard, passwordChangedGuard],
  },
  {
    path: 'profile',
    component: Profile,
    canActivate: [authGuard, passwordChangedGuard],
  },
  {
    path: 'change-password',
    component: ChangePassword,
    canActivate: [authGuard],
  },
  {
    path: 'new-employee',
    component: NewEmployee,
    canActivate: [authGuard, passwordChangedGuard, hrOrAdminGuard],
  },
  {
    path: 'employees',
    component: Employees,
    canActivate: [authGuard, passwordChangedGuard, hrOrAdminGuard],
  },
  {
    path: 'employees/:id',
    component: EmployeeDetails,
    canActivate: [authGuard, passwordChangedGuard, hrOrAdminGuard],
  },
];
