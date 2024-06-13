import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Title } from '../models/catalog-item.model';

@Injectable({
  providedIn: 'root'
})
export class CatalogService {
  private apiUrl: string = 'http://localhost:5025/api/titles';

  constructor(
    private http: HttpClient
  ) { }

  getAll(): Observable<Title[]> {
    return this.http.get<Title[]>(this.apiUrl);
  }

  getById(id: string): Observable<Title> {
    return this.http.get<Title>(`${this.apiUrl}/${id}`);
  }

  download(tid: string, eid: string): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/${tid}/episodes/${eid}/download`, {
      responseType: 'blob'
    });
  }

  thumbnail(tid: string): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/${tid}/thumbnail`, {
      responseType: 'blob'
    });
  }

  search(query: string): Observable<Title[]> {
    return this.http.get<Title[]>(`${this.apiUrl}/search?query=${query}`);
  }
}