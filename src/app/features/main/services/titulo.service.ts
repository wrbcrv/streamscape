import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Titulo } from '../../../core/models/titulo.model';

@Injectable({
  providedIn: 'root'
})
export class TituloService {
  private apiUrl: string = 'http://localhost:8080/api/titulos';

  constructor(private http: HttpClient) { }

  getAll(): Observable<Titulo[]> {
    return this.http.get<Titulo[]>(this.apiUrl);
  }

  downloadImage(id: number) {
    return this.http.get(`${this.apiUrl}/${id}/thumb/download`, {
      responseType: 'blob'
    });
  }
}