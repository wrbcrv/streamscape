import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { User } from '../../../core/models/user.model';
import { NavigationEnd, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { filter } from 'rxjs';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {
  user: User | null = null;
  visible: boolean = false;
  header: boolean = true;

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.authService.user.subscribe(
      (user) => {
        this.user = user;
      }
    );

    this.router.events.pipe(filter(event => event instanceof NavigationEnd)).subscribe(() => {
      const current = this.router.url;
      this.header = !['/login', '/register'].includes(current);
      this.close();
    });
  }

  toggle(): void {
    this.visible = !this.visible;
  }

  close(event?: MouseEvent): void {
    if (event && (event.target as HTMLElement).classList.contains('menu-overlay')) {
      this.visible = false;
    } else if (!event) {
      this.visible = false;
    }
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}