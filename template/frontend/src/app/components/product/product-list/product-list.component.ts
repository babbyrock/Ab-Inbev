import { Component, inject, OnInit } from '@angular/core';
import { ProductService } from '../../../services/product.service';
import { Product } from '../../../models/product';
import { ProductQueryParams } from '../../../models/product-query';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../../../shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-product-list',
  standalone: true,
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss'],
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatButtonModule,
    RouterModule,
    MatCardModule
  ]
})
export class ProductListComponent implements OnInit {

  private productService = inject(ProductService);
  private dialog = inject(MatDialog);
  private router = inject(Router);

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
    const dialogRef = this.dialog.open(ConfirmDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.productService.deleteProduct(id).subscribe(() => {
          this.loadProducts();
        });
      }
    });
  }

  onPageChange(event: any): void {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadProducts();
  }

  viewProduct(id: number): void {
    this.router.navigate(['/products/details', id]);
  }

}
