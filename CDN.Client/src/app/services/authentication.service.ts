import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, map, Observable, ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  baseUrl: string = environment.apiUrl;
  private currentUserSource = new ReplaySubject<any>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) {}

  login(model: any) {
    return this.http
      .post<string>(this.baseUrl + 'Authentication/Login', model)
      .pipe(
        map((token) => {
          localStorage.setItem('token', JSON.stringify(token));
          return token;
        })
      );
  }
}
