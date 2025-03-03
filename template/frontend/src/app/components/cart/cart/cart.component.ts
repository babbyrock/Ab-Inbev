import { Component, OnInit, inject } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CartItem } from '../../../models/cart';
import { CartService } from '../../../services/cart.service';
import { AuthService } from '../../../services/auth.service';
import { ProductService } from '../../../services/product.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { Product } from '../../../models/product';
import { forkJoin } from 'rxjs';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-cart',
  standalone: true,
  templateUrl: './cart.component.html',
  imports: [CommonModule, FormsModule, MatCardModule, MatButtonModule],
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  cartItems: CartItem[] = [];
  cartId: number | undefined;
  products: Product[] = [];
  quantities: { [key: number]: number } = {};

  private cartService = inject(CartService);
  private authService = inject(AuthService);
  private snackBar = inject(MatSnackBar);
  private productService = inject(ProductService);

  ngOnInit() {
    const userId = this.authService.getUserId().toString();
    this.cartService.getCartByUserId(userId).subscribe({
      next: (cart: any) => {
        this.cartItems = cart.data.products;
        this.cartId = cart.data.id;


        const productRequests = this.cartItems.map(item => this.productService.getProductById(item.productId));
        forkJoin(productRequests).subscribe({
          next: (responses) => {
            this.products = responses.map((response: any) => response.data);
            this.initializeQuantities();
          },
          error: () => {
            this.snackBar.open('Erro ao carregar os detalhes dos produtos', 'Fechar', {
              duration: 3000,
            });
          }
        });
      },
      error: () => {
        this.snackBar.open('Erro ao carregar o carrinho', 'Fechar', {
          duration: 3000,
        });
      }
    });
  }


  initializeQuantities() {
    this.cartItems.forEach(item => {
      this.quantities[item.productId] = item.quantity || 1;
    });
  }


  updateQuantity(productId: number) {
    const item = this.cartItems.find(item => item.productId === productId);
    if (item) {
      item.quantity = this.quantities[productId];
    }

    const userId = this.authService.getUserId().toString();
    this.cartService.updateCart(this.cartId!, userId, this.cartItems).subscribe({
      next: () => {
        this.snackBar.open('Carrinho atualizado com sucesso!', 'Fechar', {
          duration: 3000,
        });
      },
      error: () => {
        this.snackBar.open('Erro ao atualizar o carrinho', 'Fechar', {
          duration: 3000,
        });
      }
    });
  }


  removeItem(productId: number) {

    this.cartItems = this.cartItems.filter(item => item.productId !== productId);

    const userId = this.authService.getUserId().toString();

    if (this.cartItems.length === 0) {
      this.deleteCart(this.cartId!);
      this.products = this.products.filter(item => item.id !== productId);
    }{
      this.cartService.updateCart(this.cartId!, userId, this.cartItems).subscribe({
        next: () => {
          this.snackBar.open('Produto removido com sucesso!', 'Fechar', {
            duration: 3000,
          });

          this.products = this.products.filter(item => item.id !== productId);

        },
        error: () => {
          this.snackBar.open('Erro ao remover o produto', 'Fechar', {
            duration: 3000,
          });
        }
      });
    }


  }


  deleteCart(cartId: number) {
    this.cartService.deleteCart(cartId).subscribe({
      next: () => {
        this.cartItems = [];
        this.products = [];
        this.quantities = {};
        this.snackBar.open('Carrinho excluído com sucesso!', 'Fechar', {
          duration: 3000,
        });
      },
      error: () => {
        this.snackBar.open('Erro ao excluir o carrinho', 'Fechar', {
          duration: 3000,
        });
      }
    });
  }


  clearCart() {
    if (this.cartId) {
      this.cartService.deleteCart(this.cartId).subscribe({
        next: () => {
          this.cartItems = [];
          this.products = [];
          this.quantities = {};
          this.snackBar.open('Carrinho excluído com sucesso!', 'Fechar', {
            duration: 3000,
          });
        },
        error: () => {
          this.snackBar.open('Erro ao excluir o carrinho', 'Fechar', {
            duration: 3000,
          });
        }
      });
    }
  }
}
