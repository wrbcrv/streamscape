import { Component, OnInit } from '@angular/core';
import { User } from '../../../models/user.model';
import { AuthService } from '../../../services/auth.service';
import { UserService } from '../../../services/user.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-account',
  standalone: true,
  imports: [
    FormsModule
  ],
  templateUrl: './account.component.html',
  styleUrl: './account.component.scss'
})
export class AccountComponent implements OnInit {
  user: User | null = null;
  email: string = '';
  usern: string = '';
  curPw: string = '';
  passw: string = '';
  rptPw: string = '';
  messages: any = null;


  constructor(
    private authService: AuthService,
    private userService: UserService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.authService.me().subscribe(
      (res) => {
        this.user = res;
        if (this.user) {
          this.email = this.user.email;
          this.usern = this.user.username;
        }
      },
      (err) => {
        console.log(err);
      }
    );
  }

  updateAccount() {
    if (this.user) {
      this.userService.updateUser(this.user.id, this.email, this.usern, this.curPw, this.passw, this.rptPw).subscribe(
        (res) => {
          console.log(res);
          if (this.user) {
            this.user.email = this.email;
            this.user.username = this.usern;

            this.authService.logout();
            this.router.navigate(['/login']);
          }
        },
        (err) => {
          console.log(err);

          if (err.status === 400 && err.error && err.error.errors) {
            this.messages = err.error.errors;
            console.log(this.messages);
          }
        }
      );
    }
  }

  errors(field: string): string[] {
    if (this.messages && this.messages[field])
      return this.messages[field];

    return [];
  }
}