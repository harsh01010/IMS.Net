import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AddressService {
  constructor(private http: HttpClient) {}

  getUserAddresses(userId:string): Observable<any> {
   
    return this.http.get(`${environment.baseAPI}/api/ShippingAddress/${userId}`);
  }

  addAddress(address: any,userId:string): Observable<any> {
   
    return this.http.post(`${environment.baseAPI}/api/ShippingAddress/${userId}`, address);
  }
}
