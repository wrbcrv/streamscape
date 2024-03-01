import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { ErrorHandlerService } from '../../../../core/services/error-handler.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  request: any = {
    nome: '',
    sobrenome: '',
    username: '',
    email: '',
    senha: ''
  }

  errors: { [key: string]: string } = {};

  constructor(
    private authService: AuthService,
    private router: Router,
    private errorHandlerService: ErrorHandlerService) { }

  onSubmit(): void {
    this.authService.register(this.request).subscribe((response) => {
      this.authService.login(this.request.email, this.request.senha).subscribe((response) => {
        this.router.navigateByUrl('/')
      }, (error) => {
        console.log(error);
      })

      console.log(response);
    }, (error) => {
      console.log(error);
    })
  }
}