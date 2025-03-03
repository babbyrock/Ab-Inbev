import { Routes } from '@angular/router';
import { ProductListComponent } from './components/product/product-list/product-list.component';
import { ProductCreateComponent } from './components/product/product-create/product-create.component';
import { ProductDetailsComponent } from './components/product/product-details/product-details.component';
import { HomeComponent } from './components/home/home.component';
import { UserRegisterComponent } from './components/user/user-register/user-register.component';
import { AuthGuard } from './guards/auth.guard'; // Importando o AuthGuard
import { LoginComponent } from './components/login/login.component';
import { CartComponent } from './components/cart/cart/cart.component';

// Definindo as rotas para a aplicação
export const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'products', component: ProductListComponent, canActivate: [AuthGuard] },
  { path: 'products/create', component: ProductCreateComponent, canActivate: [AuthGuard] },
  { path: 'products/edit/:id', component: ProductDetailsComponent, canActivate: [AuthGuard] },
  { path: 'products/details/:id', component: ProductDetailsComponent },
  { path: 'create-user', component: UserRegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'cart', component: CartComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
];
