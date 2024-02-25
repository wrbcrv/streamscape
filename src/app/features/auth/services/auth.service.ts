import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  apiUrl: string = 'http://localhost:8080/api/login';

  constructor(private http: HttpClient) { }

  login(email: string, senha: string): Observable<any> {
    const request = {
      email: email,
      senha: senha
    };

    return this.http.post<any>(this.apiUrl, request, {
      observe: 'response',
      withCredentials: true
    });
  }
}