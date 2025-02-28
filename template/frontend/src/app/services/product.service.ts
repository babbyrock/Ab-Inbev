import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProductQueryParams } from '../models/product-query';
import { Product } from '../models/product';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root',
})
export class ProductService {

  private apiUrl = `${environment.apiUrl}/api/products`;

  constructor(private http: HttpClient) {}

  getProducts(queryParams: ProductQueryParams): Observable<any> {
    let params = new HttpParams()
      .set('pageNumber', queryParams.pageNumber.toString())
      .set('pageSize', queryParams.pageSize.toString());

    params = queryParams.title ? params.set('title', queryParams.title) : params;
    params = queryParams.minPrice != null ? params.set('minPrice', queryParams.minPrice.toString()) : params;
    params = queryParams.maxPrice != null ? params.set('maxPrice', queryParams.maxPrice.toString()) : params;
    params = queryParams.category ? params.set('category', queryParams.category) : params;
    params = queryParams.description ? params.set('description', queryParams.description) : params;
    params = queryParams.image ? params.set('image', queryParams.image) : params;
    params = queryParams.count != null ? params.set('count', queryParams.count.toString()) : params;
    params = queryParams.rate != null ? params.set('rate', queryParams.rate.toString()) : params;

    return this.http.get<any>(this.apiUrl, { params });
  }


  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/${id}`);
  }

  createProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(this.apiUrl, product);
  }

  updateProduct(id: number, product: Product): Observable<Product> {
    return this.http.put<Product>(`${this.apiUrl}/${id}`, product);
  }

  deleteProduct(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
