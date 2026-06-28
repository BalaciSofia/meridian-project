import { Routes } from '@angular/router';
import { Calendar } from './pages/calendar/calendar';
import { Employees } from './pages/employees/employees';
import { Home } from './pages/home/home';
import { Login } from './pages/login/login';
import { NewEmployee } from './pages/new-employee/new-employee';
import { Profile } from './pages/profile/profile';

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
  },
  {
    path: 'calendar',
    component: Calendar,
  },
  {
    path: 'profile',
    component: Profile,
  },
  {
    path: 'new-employee',
    component: NewEmployee,
  },
  {
    path: 'employees',
    component: Employees,
  },
];
