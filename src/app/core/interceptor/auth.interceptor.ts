import { HttpClient, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map, switchMap } from "rxjs/operators";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private apiUrl: string = 'http://localhost:8080/api/login';

  constructor(private http: HttpClient) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (req.url === this.apiUrl || req.url === `${this.apiUrl}/token` || req.url === 'http://localhost:8080/api/usuarios')
      return next.handle(req);

    return this.getToken().pipe(
      switchMap(token => {
        if (token) {
          req = req.clone({
            setHeaders: {
              Authorization: `Bearer ${token}`
            }
          });
        }

        return next.handle(req);
      })
    );
  }

  private getToken(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/token`, {
      withCredentials: true
    }).pipe(
      map(response => response.token)
    );
  }
}