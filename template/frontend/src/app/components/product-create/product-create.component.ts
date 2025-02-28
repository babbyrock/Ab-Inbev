import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatButtonModule } from '@angular/material/button';
import { BrowserModule } from '@angular/platform-browser';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'app-product-create',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, MatTableModule,
    BrowserModule,
    MatFormFieldModule,
    MatSelectModule,
    MatButtonModule,
    BrowserAnimationsModule],  // Importando ReactiveFormsModule aqui
  templateUrl: './product-create.component.html',
  styleUrls: ['./product-create.component.css'],
})
export class ProductCreateComponent {
  private productService = inject(ProductService);
  private router = inject(Router);
  private fb = inject(FormBuilder);

  productForm: FormGroup;

  constructor() {
    this.productForm = this.fb.group({
      title: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
      price: ['', [Validators.required, Validators.min(0)]],
      description: ['', Validators.required],
      category: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      image: ['', [Validators.required, Validators.pattern(/^.*\.(jpg|jpeg|png|gif|bmp)$/i)]],
      rating: [5, [Validators.required, Validators.min(0), Validators.max(5)]],
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
          rate: productData.rating || 0
        }
      };

      this.productService.createProduct(formattedData).subscribe(
        (response) => {
          console.log('Produto criado com sucesso', response);
          this.router.navigate(['/products']);
        },
        (error) => {
          console.error('Erro ao criar produto', error);
        }
      );
    }
  }
}
