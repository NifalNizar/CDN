import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  baseUrl: string = environment.apiUrl;
  constructor(private http: HttpClient, private router: Router) {}

  getAll(): Observable<User[]> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem('token'),
    });
    // headers.append('Content-Type', 'application/json');
    // headers.append('Authorization', 'Bearer ' + localStorage.getItem('token'));

    return this.http.get<User[]>(this.baseUrl + 'Users', {
      headers: headers,
    });
  }
}
