import { HttpClient, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { map, switchMap } from "rxjs/operators";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private apiUrl: string = 'http://localhost:8080/api/login';

  private allowedUrls: string[] = [
    this.apiUrl,
    `${this.apiUrl}/token`,
    'http://localhost:8080/api/usuarios'
  ];

  constructor(private http: HttpClient) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (this.isAllowedUrl(req.url))
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

  private isAllowedUrl(url: string): boolean {
    return this.allowedUrls.some(allowedUrl => url === allowedUrl);
  }
}