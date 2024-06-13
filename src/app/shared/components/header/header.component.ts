import { Component, HostListener, OnInit } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { User } from '../../../core/models/user.model';
import { NavigationEnd, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { filter } from 'rxjs';
import { SearchComponent } from '../../../features/catalog/components/search/search.component';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    SearchComponent
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {
  user: User | null = null;
  visible: boolean = false;
  header: boolean = true;
  transparent: boolean = false;
  isSearchOpen: boolean = false;

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
      this.header = !['/login', '/register'].includes(current) && !/^\/watch\/\w+\/\w+/.test(current);
      this.close();
    });

    this.onWindowScroll();
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

  @HostListener('window:scroll', [])
  onWindowScroll() {
    const offset = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || 0;
    this.transparent = offset === 0 && !['/login', '/register'].includes(this.router.url);
  }

  openSearch(): void {
    this.isSearchOpen = true;
    document.body.classList.add('scroll');
  }

  closeSearch(): void {
    this.isSearchOpen = false;
    document.body.classList.remove('scroll');
  }
}