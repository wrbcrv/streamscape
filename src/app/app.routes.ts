import { Routes } from '@angular/router';
import { LoginComponent } from './features/auth/components/login/login.component';
import { RegisterComponent } from './features/auth/components/register/register.component';
import { CatalogListComponent } from './features/catalog/components/catalog-list/catalog-list.component';
import { ItemDetailsComponent } from './features/catalog/components/item-details/item-details.component';
import { PlayerComponent } from './features/catalog/components/player/player.component';
import { itemResolver } from './features/catalog/services/item.resolver';

export const routes: Routes = [
  {
    path: '',
    component: CatalogListComponent,
    title: 'Home – Streamscape'
  },
  {
    path: 'login',
    component: LoginComponent,
    title: 'Streamscape – Entrar'
  },
  {
    path: 'register',
    component: RegisterComponent,
    title: 'Streamscape – Criar conta'
  },
  {
    path: 'title/:id',
    component: ItemDetailsComponent,
    resolve: {
      item: itemResolver
    }
  },
  {
    path: 'watch/:tid/:eid',
    component: PlayerComponent,
  },
];