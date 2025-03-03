

export interface Cart {
    id: number;
    userId: string;
    date: Date;
    products: CartItem[];
}

export interface CartItem {
  id?: number;
  productId: number;
  quantity: number;
}
