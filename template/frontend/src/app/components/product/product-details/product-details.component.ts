import { Component, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../../services/product.service';
import { MatCardModule } from '@angular/material/card';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    ReactiveFormsModule,
    CommonModule
  ],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent {
  private route = inject(ActivatedRoute);
  private productService = inject(ProductService);
  private snackBar = inject(MatSnackBar);
  private fb = inject(FormBuilder);
  private router = inject(Router);

  productForm!: FormGroup;
  productId!: number;
  isEditMode: boolean = false;

  ngOnInit() {
    this.productForm = this.fb.group({
      title: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
      price: ['', [Validators.required, Validators.min(0.01)]],
      description: ['', [Validators.required]],
      category: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      image: ['', [Validators.required]],
      rating: this.fb.group({
        rate: ['', [Validators.required, Validators.min(1), Validators.max(5)]],
        count: ['', [Validators.min(0)]]
      })
    });

    this.route.paramMap.subscribe(params => {
      this.productId = Number(params.get('id'));

      if (this.productId) {
        this.isEditMode = this.router.url.includes('edit');
        if (this.isEditMode) {
          this.initEdit();
        } else {
          this.initDetail();
        }
      }
    });
  }

  initDetail() {
    this.productService.getProductById(this.productId).subscribe({
      next: (data: any) => {
        const mappedData = { ...data.data };
        this.productForm.patchValue(mappedData);
      },
      error: () => {
        this.snackBar.open('Erro ao buscar produto', 'Fechar', {
          duration: 3000,
        });
      }
    });
  }

  initEdit() {
    this.productService.getProductById(this.productId).subscribe({
      next: (data: any) => {
        const mappedData = { ...data.data };
        this.productForm.patchValue(mappedData);
        this.productForm.enable();
      },
      error: () => {
        this.snackBar.open('Erro ao buscar produto para editar', 'Fechar', {
          duration: 3000,
        });
      }
    });
  }

  navigateToProducts() {
    this.router.navigate(['/products']);
  }

  saveProduct() {
    if (this.productForm.valid) {
      this.productService.updateProduct(this.productId, this.productForm.value).subscribe({
        next: () => {
          this.snackBar.open('Produto atualizado com sucesso!', 'Fechar', {
            duration: 3000,
          });
          this.router.navigate(['/products']);
        },
        error: () => {
          this.snackBar.open('Erro ao atualizar produto', 'Fechar', {
            duration: 3000,
          });
        }
      });
    }
  }
}
