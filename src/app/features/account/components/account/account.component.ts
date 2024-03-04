import { Component, OnInit } from '@angular/core';
import { Usuario } from '../../../../core/models/usuario.model';
import { UsuarioService } from '../../../../core/services/usuario.service';

@Component({
  selector: 'app-account',
  standalone: true,
  imports: [],
  templateUrl: './account.component.html',
  styleUrl: './account.component.scss'
})
export class AccountComponent implements OnInit {
  usuario: Usuario | null = null;

  constructor(private usuarioService: UsuarioService) { }

  ngOnInit(): void {
    this.getLoggedInUsuario();
  }

  getLoggedInUsuario(): void {
    this.usuarioService.getLoggedInUsuario().subscribe((response) => {
      this.usuario = response;
    }, (error) => {

    })
  }
}