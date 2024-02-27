import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router, RouterModule } from '@angular/router';
import { UsuarioService } from '../../../core/services/usuario.service';
import { Usuario } from '../../../core/models/usuario.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {
  usuario: Usuario | null = null;

  constructor(private usuarioService: UsuarioService, private router: Router) {
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
    this.usuarioService.getLoggedInUsuario().subscribe((response) => {
      this.usuario = response;
    }, (error) => {

    })
  }
}