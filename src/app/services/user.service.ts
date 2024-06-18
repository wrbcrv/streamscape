import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl: string = 'http://localhost:5025/api/users';

  constructor(
    private http: HttpClient
  ) { }

  addToMyList(uid: number, tid: number): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/${uid}/titles/${tid}`, {});
  }
}