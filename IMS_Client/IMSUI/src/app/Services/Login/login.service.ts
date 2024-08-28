import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { LoginCredentials } from '../../Models/Login.model';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private http: HttpClient) { }

  login(loginCredentials: LoginCredentials): Observable<any> {
    return this.http.post<any>(`${environment.baseAPI}/api/AuthApi/Login`, loginCredentials).pipe(
      catchError(error => {
        console.error('Login error', error);
        return of({ success: false });
      })
    );
  }
}
