import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { CartService } from '../Cart/cart.service';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  constructor(private http: HttpClient, private cartservice: CartService) { }


  deleteCart = async (cartId: string) => {
    await this.cartservice.deleteCart(cartId);
  }
  placeOrder(address: any, cartId: string): Observable<any> {


    return this.http.post(`${environment.baseAPI}/api/Orders/placeOrder/${cartId}`, {

      address: address
    });
  }
  getallOrders(): Observable<any> {
    return this.http.get(`${environment.baseAPI}/api/Orders/getAllOrders`);
  }

  getOrderHistory(id: string): Observable<any> {
    return this.http.get(`${environment.baseAPI}/api/Orders/getOrderHistory/${id}`);
  }
}
