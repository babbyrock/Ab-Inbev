import { Component, inject, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product';
import { ProductQueryParams } from '../../models/product-query';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-product-list',
  standalone: true,
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css'],
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatButtonModule,
    RouterModule,
  ]

})
export class ProductListComponent implements OnInit {

  private productService = inject(ProductService);
  products: Product[] = [];
  totalProducts: number = 0;
  pageSize: number = 10;
  pageNumber: number = 1;
  displayedColumns: string[] = ['title', 'price', 'actions'];


  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    const queryParams: ProductQueryParams = {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
    };
    this.productService.getProducts(queryParams).subscribe(response => {
      this.products = response.data.items;
      this.totalProducts = response.data.totalCount;
    });
  }

  deleteProduct(id: number): void {
    this.productService.deleteProduct(id).subscribe(() => {
      this.loadProducts();
    });
  }

  onPageChange(event: any): void {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadProducts();
  }
}
