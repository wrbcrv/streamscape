import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

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

  constructor(private authService: AuthService) { }

  login(): void {
    if (this.request) {
      this.authService.login(this.request.email, this.request.senha).subscribe((response) => {
        console.log(response);
      }, (error) => {
        console.log(error);
      });
    }
  }
}