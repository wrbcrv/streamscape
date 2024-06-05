import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Register } from '../../../../core/models/register.model';
import { AuthService } from '../../../../core/services/auth.service';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    FormsModule,
    RouterModule
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  messages: { [key: string]: string } = {};
  form: Register = {
    email: '',
    username: '',
    password: '',
    repeat: '',
    role: 'User'
  }

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  submit(): void {
    this.messages = {};

    this.authService.register(this.form).subscribe(
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