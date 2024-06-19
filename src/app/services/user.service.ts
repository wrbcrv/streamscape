import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl: string = 'http://localhost:5025/api/users';

  constructor(
    private http: HttpClient
  ) { }

  updateUser(id: number, email?: string, usern?: string, curPw?: string, passw?: string, rptPw?: string): Observable<any> {
    const payload: any = {};

    if (email !== undefined) {
      payload.email = email;
    }

    if (usern !== undefined) {
      payload.username = usern;
    }

    if (curPw !== undefined) {
      payload.currentPassword = curPw;
    }

    if (passw !== undefined) {
      payload.newPassword = passw;
    }

    if (rptPw !== undefined) {
      payload.repeatPassword = rptPw;
    }

    return this.http.put(`${this.apiUrl}/${id}`, payload);
  }

  addToMyList(uid: number, tid: number): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/${uid}/titles/${tid}`, {});
  }

  removeFromMyList(uid: number, tid: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${uid}/titles/${tid}`);
  }
}