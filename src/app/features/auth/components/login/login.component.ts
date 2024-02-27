import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ErrorHandlerService } from '../../../../core/services/error-handler.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  request = {
    email: '',
    senha: ''
  };

  errors: { [key: string]: string } = {};

  showPassword: boolean = false;

  constructor(
    private authService: AuthService,
    private errorHandlerSevice: ErrorHandlerService,
    private router: Router) { }

  login(): void {
    if (this.request) {
      this.authService.login(this.request.email, this.request.senha).subscribe((response) => {
        this.router.navigateByUrl('/');
      }, (error) => {
        this.errors = this.errorHandlerSevice.handleErrors(error);
      });
    }
  }

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }
}