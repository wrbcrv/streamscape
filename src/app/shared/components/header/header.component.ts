import { CommonModule } from '@angular/common';
import { Component, HostListener, OnInit } from '@angular/core';
import { NavigationEnd, Router, RouterModule } from '@angular/router';
import { filter } from 'rxjs';
import { User } from '../../../core/models/user.model';
import { AuthService } from '../../../core/services/auth.service';
import { SearchComponent } from '../../../features/catalog/components/search/search.component';
import { MenuComponent } from '../menu/menu.component';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    SearchComponent,
    MenuComponent
  ],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  user: User | null = null;
  header: boolean = true;
  transparent: boolean = false;
  search: boolean = false;
  menu: boolean = false;

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
      this.closeMenu();
    });

    this.onWindowScroll();
  }

  toggleMenu(): void {
    this.menu = !this.menu;
  }

  closeMenu(): void {
    this.menu = false;
  }

  @HostListener('window:scroll', [])
  onWindowScroll() {
    const offset = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || 0;
    this.transparent = offset === 0 && !['/login', '/register'].includes(this.router.url);
  }

  openSearch(): void {
    this.search = true;
    document.body.classList.add('scroll');
  }

  closeSearch(): void {
    this.search = false;
    document.body.classList.remove('scroll');
  }
}