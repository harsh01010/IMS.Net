import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  constructor(private http: HttpClient) {}

  placeOrder(address: any): Observable<any> {
    const userId = 'cb48ba81-67fe-4e04-9fcb-8c5411080dee'; // Replace with actual user ID
    return this.http.post(`${environment.baseAPI}/api/Orders/placeOrder/${userId}`, {
     
      address: address
    });
  }
}
