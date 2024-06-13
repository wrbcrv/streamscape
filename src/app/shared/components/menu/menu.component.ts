import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { User } from '../../../core/models/user.model';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.scss'
})
export class MenuComponent implements OnInit {
  user: User | null = null;

  constructor(
    private authService: AuthService
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
    this.closeMenu.emit();
  }
}