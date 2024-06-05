import { Routes } from '@angular/router';
import { LoginComponent } from './features/auth/components/login/login.component';
import { RegisterComponent } from './features/auth/components/register/register.component';

export const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
    title: 'Streamscape – Entrar'
  },
  {
    path: 'register',
    component: RegisterComponent,
    title: 'Streamscape – Criar conta'
  }
];