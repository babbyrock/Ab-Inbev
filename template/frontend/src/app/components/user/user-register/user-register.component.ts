import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';  // Importando o ReactiveFormsModule
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserRole, UserRoleNames } from '../../../enums/user-role';
import { UserStatus, UserStatusNames } from '../../../enums/user-status';
import { UserService } from '../../../services/user.service';
import { MatIconModule } from '@angular/material/icon';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-register',
  standalone: true,
  imports: [CommonModule, MatInputModule, MatButtonModule, MatSelectModule, FormsModule, ReactiveFormsModule, MatIconModule ],
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.scss']
})
export class UserRegisterComponent {
  userForm: FormGroup;
  userRoles = Object.entries(UserRole)
    .filter(([key, value]) => typeof value === 'number')
    .map(([key, value]) => ({ key: value, value: UserRoleNames[value as UserRole] }));
  userStatuses = Object.entries(UserStatus)
  .filter(([key, value]) => typeof value === 'number')
  .map(([key, value]) => ({ key: value, value: UserStatusNames[value as UserStatus] }));

  hidePassword: boolean = true;

  private userService = inject(UserService);
  private snackBar = inject(MatSnackBar);
  private router = inject(Router);

  constructor(private fb: FormBuilder) {
    this.userForm = this.fb.group({
      username: ['', [Validators.required, Validators.minLength(3)]],
      password: ['', [
        Validators.required,
        Validators.minLength(8),
        Validators.pattern('^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[!@#$%^&*])[A-Za-z0-9!@#$%^&*]{8,}$')
      ]],
      phone: ['', [Validators.required, Validators.pattern(/^[0-9]{10,11}$/)]],
      email: ['', [Validators.required, Validators.email]],
      status: [UserStatus.Unknown, [Validators.required]],
      role: [UserRole.None, [Validators.required]]
    });
  }

  submitForm(): void {
    if (this.userForm.valid) {
      const formValue = this.userForm.value;

      const roleValue = formValue.role;
      const statusValue = formValue.status;

      const user = {
        ...formValue,
        status: statusValue,
        role: roleValue
      };

      this.userService.createUser(user).subscribe({
        next: () => {
          this.snackBar.open('Usuário registrado com sucesso!', 'Fechar', {
            duration: 3000,
            panelClass: ['snack-success']
          });
          this.router.navigate(['/']);
        },
        error: () => {
          this.snackBar.open('Erro ao registrar o usuário. Tente novamente.', 'Fechar', {
            duration: 3000,
            panelClass: ['snack-error']
          });
        }
      });
    } else {
      this.snackBar.open('Formulário inválido. Verifique os campos.', 'Fechar', {
        duration: 3000,
        panelClass: ['snack-error']
      });
    }
  }

  togglePasswordVisibility(): void {
    this.hidePassword = !this.hidePassword;
  }
}

