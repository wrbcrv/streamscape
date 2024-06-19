import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { User } from '../../../models/user.model';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [
    RouterModule
  ],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.scss'
})
export class MenuComponent implements OnInit {
  user: User | null = null;

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.authService.user.subscribe(
      (res) => {
        this.user = res;
      },
      (err) => {
        throw Error(err);
      }
    );
  }

  @Output() closeMenu = new EventEmitter<void>();

  close(event: MouseEvent): void {
    if ((event.target as HTMLElement).classList.contains('menu-overlay')) {
      this.closeMenu.emit();
    }
  }

  logout(): void {
    this.authService.logout()
    this.router.navigate(['/login']);
  }
}