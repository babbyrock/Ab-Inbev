import { Routes } from '@angular/router';
import { ProductListComponent } from './components/product-list/product-list.component';
import { ProductCreateComponent } from './components/product-create/product-create.component';
import { ProductEditComponent } from './components/product-edit/product-edit.component';
import { ProductDetailsComponent } from './components/product-details/product-details.component';

// Definindo as rotas para a aplicação
export const routes: Routes = [
  { path: 'products', component: ProductListComponent },
  { path: 'products/create', component: ProductCreateComponent },
  { path: 'products/edit/:id', component: ProductEditComponent },
  { path: 'products/details/:id', component: ProductDetailsComponent },
  { path: '', redirectTo: '/products', pathMatch: 'full' },
];
