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
}