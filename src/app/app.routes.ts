import { Routes } from '@angular/router';
import { AccountComponent } from './features/account/components/account/account.component';
import { LoginComponent } from './features/auth/components/login/login.component';
import { RegisterComponent } from './features/auth/components/register/register.component';
import { CatalogComponent } from './features/main/components/catalog/catalog.component';

export const routes: Routes = [
  {
    path: '',
    component: CatalogComponent,
    title: 'Home – Streamscape'
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'account',
    component: AccountComponent
  }
];