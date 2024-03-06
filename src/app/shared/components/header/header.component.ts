import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router, RouterModule } from '@angular/router';
import { ClickOutsideModule } from 'ng-click-outside';
import { Usuario } from '../../../core/models/usuario.model';
import { UsuarioService } from '../../../core/services/usuario.service';
import { AuthService } from '../../../features/auth/services/auth.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    ClickOutsideModule
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {
  usuario: Usuario | null = null;
  menuOpen: boolean = false;

  constructor(
    private usuarioService: UsuarioService,
    private authService: AuthService,
    private router: Router) {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd && event.url === '/') {
        this.getLoggedInUsuario();
      }
    });
  }

  ngOnInit(): void {
    if (this.router.url === '/') {
      this.getLoggedInUsuario();
    }
  }

  getLoggedInUsuario(): void {
    this.usuarioService.getLoggedInUsuario().subscribe((res) => {
      this.usuario = res;
    }, () => {

    })
  }

  logout(): void {
    this.authService.logout().subscribe(() => {
      this.router.navigateByUrl('/login');
    }, () => {

    })
  }

  toggleMenu(): void {
    this.menuOpen = !this.menuOpen;
  }

  onClickOutside(): void {
    this.menuOpen = false;
  }
}