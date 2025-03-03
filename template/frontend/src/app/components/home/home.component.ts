import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { Product } from '../../models/product';
import { ProductService } from '../../services/product.service';
import { ProductQueryParams } from '../../models/product-query';
import { AuthService } from '../../services/auth.service';
import { CartService } from '../../services/cart.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Cart, CartItem } from '../../models/cart';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  products: Product[] = [];
  pageNumber: number = 1;
  pageSize: number = 10; // 15 produtos por página (3 linhas de 5 produtos)
  totalProducts: number = 0;
  totalPages: number = 0;

  private productService = inject(ProductService);
  private cartService = inject(CartService);
  private authService = inject(AuthService);
  private snackBar = inject(MatSnackBar);
  cart: Cart | null = null;

  constructor() {
    this.loadProducts('');
  }

  loadProducts(direction: string): void {
    if (direction === 'next') {
      this.pageNumber++;
    } else if (direction === 'previous') {
      this.pageNumber--;
    }

    const queryParams: ProductQueryParams = {
      pageNumber: this.pageNumber,
      pageSize: 10,
    };

    this.productService.getProducts(queryParams).subscribe(response => {
      this.products = response.data.items;
      this.totalProducts = response.data.totalCount;
      this.totalPages = Math.ceil(this.totalProducts / 10);
    });
  }


  addToCart(product: any): void {

    this.cartService.getCartByUserId(this.authService.getUserId().toString()).subscribe({
      next: (cart: any) => {
        this.cart = cart.data;

        const existingItem = this.cart?.products.find((item: any) => item.productId === product.id);
        const cartItem = {
          productId: product.id,
          quantity: existingItem ? existingItem.quantity + 1 : 1
        } as CartItem;

        const cartParam = {
          id: cart.data?.id,
          userId: this.authService.getUserId().toString(),
          cartItems: [] as CartItem[]
        }

        if (cart.data && Array.isArray(cart.data.products)) {
          cartParam.cartItems = cart.data.products;
        } else {
          cartParam.cartItems = [];
        }
        cartParam.cartItems.push(cartItem);

        if (!this.cart || !this.cart.products || this.cart.products.length === 0) {

          this.cartService.addToCart(cartParam.userId, cartParam.cartItems ).subscribe({
            next: (response) => {
              this.snackBar.open('Produto adicionado ao carrinho!', 'Fechar', {
                duration: 3000,
              });
            },
            error: (err) => {
              this.snackBar.open('Erro ao adicionar produto ao carrinho', 'Fechar', {
                duration: 3000,
              });
            }
          });
        } else {
          this.cartService.updateCart(cartParam.id, cartParam.userId, cartParam.cartItems).subscribe({
            next:() => {
              this.snackBar.open('Produto adicionado ao carrinho!', 'Fechar', {
                duration: 3000,
              });
            },
            error: (err) => {
              this.snackBar.open('Erro ao adicionar produto ao carrinho', 'Fechar', {
                duration: 3000,
              });
            }
          })
        }
      },
      error: (err) => {
        this.snackBar.open('Erro ao carregar o carrinho', 'Fechar', {
          duration: 3000,
        });
      }
    });
  }
}
