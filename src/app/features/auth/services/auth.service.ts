import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Usuario } from '../../../core/models/usuario.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  apiUrl: string = 'http://localhost:8080/api';

  constructor(private http: HttpClient) { }

  login(email: string, senha: string): Observable<any> {
    const request = {
      email: email,
      senha: senha
    };

    return this.http.post<any>(`${this.apiUrl}/login`, request, {
      observe: 'response',
      withCredentials: true
    });
  }

  logout(): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/login/logout`, {}, {
      withCredentials: true
    });
  }

  register(usuario: Usuario): Observable<Usuario> {
    const request = {
      nome: usuario.nome,
      sobrenome: usuario.sobrenome,
      username: usuario.username,
      email: usuario.email,
      senha: usuario.senha
    }

    return this.http.post<Usuario>(`${this.apiUrl}/usuarios`, request);
  }
}