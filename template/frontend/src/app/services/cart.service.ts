import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environment';
import { Cart, CartItem } from '../models/cart';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private apiUrl = `${environment.apiUrl}/api/cart`;

  constructor(private http: HttpClient) {}

  addToCart(userId: any, products: { productId: number; quantity: number }[]): Observable<any> {
    const payload = {
      userId: userId,
      products: products
    };
    return this.http.post(this.apiUrl, payload);
  }

  getCartByUserId(userId: string): Observable<Cart> {
    return this.http.get<Cart>(`${this.apiUrl}/user/${userId}`);
  }

  updateCart(cartId: number, userId: string, products: CartItem[]): Observable<any> {
    const url = `${this.apiUrl}/${cartId}`;
    const payload = {
      userId: userId,
      products: products
    };
    return this.http.put(url, payload);
  }

  deleteCart(cartId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${cartId}`);
  }
}
