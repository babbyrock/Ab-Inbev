import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ProductService } from '../../../services/product.service';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';
import {MatInputModule} from '@angular/material/input';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-product-create',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, MatTableModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule],
  templateUrl: './product-create.component.html',
  styleUrls: ['./product-create.component.scss'],
})
export class ProductCreateComponent {
  private productService = inject(ProductService);
  private router = inject(Router);
  private fb = inject(FormBuilder);
  private snackBar = inject(MatSnackBar);

  productForm: FormGroup;

  constructor() {
    this.productForm = this.fb.group({
      title: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
      price: ['', [Validators.required, Validators.min(0)]],
      description: ['', Validators.required],
      category: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      image: ['', [Validators.required]],
      rating: this.fb.group({
        rate: ['', [Validators.required, Validators.min(1), Validators.max(5)]],
        count: ['', [Validators.min(0)]]
      })
    });
  }


  onSubmit(): void {
    if (this.productForm.valid) {
      const productData = this.productForm.value;

      const formattedData = {
        title: productData.title,
        price: productData.price || 0,
        description: productData.description,
        category: productData.category,
        image: productData.image,
        rating: {
          rate: productData.rating.rate || 0,
          count: productData.rating.count || 0
        }
      };

      this.productService.createProduct(formattedData).subscribe({
        next: (product) => {
          // Se a criação for bem-sucedida, navegue para a lista de produtos
          this.router.navigate(['/products']);
        },
        error: (err) => {
          if (err.status === 400) {
            this.snackBar.open('Erro de validação. Verifique os dados inseridos.', 'Fechar', {
              duration: 3000,
            });
          } else if (err.status === 500) {
            this.snackBar.open('Erro interno no servidor. Tente novamente mais tarde.', 'Fechar', {
              duration: 3000,
            });
          } else {
            this.snackBar.open('Ocorreu um erro. Tente novamente.', 'Fechar', {
              duration: 3000,
            });
          }
        }
      });

    }
  }

  navigateToProducts() {
    this.router.navigate(['/products']);
  }
}
