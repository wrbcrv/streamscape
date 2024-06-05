import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    FormsModule,
    RouterModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  messages: { [key: string]: string } = {};
  username: string = '';
  password: string = '';

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  login(): void {
    this.messages = {};
    
    this.authService.login(this.username, this.password).subscribe(
      (res) => {
        this.router.navigateByUrl('/');
      },
      (err) => {
        this.errors(err);
      }
    )
  }

  private errors(err: any): void {
    if (err.status === 400 && err.error && err.error.errors) {
      for (const key in err.error.errors) {
        if (err.error.errors.hasOwnProperty(key)) {
          this.messages[key] = err.error.errors[key][0];
        }
      }
    } else {
      console.log(err);
    }
  }
}