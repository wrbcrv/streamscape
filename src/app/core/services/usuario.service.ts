import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Usuario } from '../models/usuario.model';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  private apiUrl: string = 'http://localhost:8080/api/usuarios';

  constructor(private http: HttpClient) { }

  getLoggedInUsuario(): Observable<Usuario> {
    return this.http.get<Usuario>(`${this.apiUrl}/me`, {
      withCredentials: true
    });
  }
}