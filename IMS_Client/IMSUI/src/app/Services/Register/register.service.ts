import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Register } from '../../Models/Register.model';
import { environment } from '../../../environments/environment';



const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})

export class RegisterService {
  constructor(private http: HttpClient) { }

  register(register:Register): Observable<any>{
    return this.http.post<any>(`${environment.baseAPI}/api/AuthApi/Register`, register)
  }
}