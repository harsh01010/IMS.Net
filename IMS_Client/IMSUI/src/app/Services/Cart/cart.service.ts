import { Inject, Injectable, PLATFORM_ID } from '@angular/core';

import { Observable } from 'rxjs';

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { CartProduct } from '../../Models/DeleteProductFromCart.model';

@Injectable({
  providedIn: 'root'
})
export class CartService {



  // Replace this with actual logic to get userId

  constructor(private http:HttpClient) {
    // No local storage operations here
    
  }
  getProductById(id: string): Observable<any> {
    return this.http.get<any>(`${environment.baseAPI}/api/Cart/get/${id}`);
  
  }
  deleteProductFromCartById(id: string,productId:CartProduct): Observable<any> {
    //return this.http.delete<any>(`${environment.baseAPI}/api/Cart/delete/${id}`,productId);


    const url = `${environment.baseAPI}/api/Cart/delete/${id}`;

    // Define the options with the body (directly passing productId object)
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json' // Set the content type to JSON
      }),
      body: productId // Directly passing the productId object in the body
    };

    // Send the DELETE request with the options including the body
    return this.http.delete<any>(url, options);
   }
     deleteCart(id:string){
      return this.http.delete<any>(`${environment.baseAPI}/api/Cart/deleteCart/${id}`);

     }

  }


