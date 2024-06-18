import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { CatalogListComponent } from './components/catalog-list/catalog-list.component';
import { ItemDetailsComponent } from './components/item-details/item-details.component';
import { PlayerComponent } from './components/player/player.component';
import { MyListComponent } from './shared/components/my-list/my-list.component';
import { itemResolver } from './components/item-details/resolver/item.resolver';

export const routes: Routes = [
  {
    path: '',
    component: CatalogListComponent,
    title: 'Home | Streamscape'
  },
  {
    path: 'login',
    component: LoginComponent,
    title: 'Streamscape | Entrar'
  },
  {
    path: 'register',
    component: RegisterComponent,
    title: 'Streamscape | Criar conta'
  },
  {
    path: 'watch/:id',
    component: ItemDetailsComponent,
    resolve: {
      item: itemResolver
    }
  },
  {
    path: 'watch/:tid/:eid',
    component: PlayerComponent,
  },
  {
    path: 'my-list',
    component: MyListComponent,
    title: 'Minha Lista | Streamscape'
  }
];