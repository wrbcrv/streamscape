import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { distinctUntilChanged } from 'rxjs/operators';
import { User } from '../models/user.model';
import { Register } from '../models/register.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl: string = 'http://localhost:5025/api';
  private $subject: BehaviorSubject<User | null>;
  public user: Observable<User | null>;

  constructor(
    private http: HttpClient
  ) {
    this.$subject = new BehaviorSubject<User | null>(null);
    this.user = this.$subject.asObservable().pipe(distinctUntilChanged());
    const token = localStorage.getItem('token');

    if (token) {
      this.me().subscribe();
    }
  }

  login(username: string, password: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/auth`, { username, password }).pipe(
      tap(res => {
        if (res.token) {
          localStorage.setItem('token', res.token);
          this.me().subscribe(user => this.$subject.next(user));
        }
      })
    );
  }

  register(register: Register): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}/users`, register).pipe(
      tap(user => {
        this.$subject.next(user);
      })
    );
  }

  me(): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/auth/me`).pipe(
      tap(user => {
        this.$subject.next(user);
      })
    );
  }

  logout(): void {
    localStorage.removeItem('token');
    this.$subject.next(null);
  }

  get value(): User | null {
    return this.$subject.value;
  }
}