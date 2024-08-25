import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../../Models/Product.model';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http:HttpClient) { }

  getallProducts(): Observable<any>{
    return this.http.get<any>(`${environment.baseAPI}/api/ProductAPI`)
  }
  getProductPage(pageNum:number,pageSize:number):Observable<any>{
    let params = new HttpParams()
    .set('pageNum',pageNum)
    .set('pageSize',pageSize);
    return this.http.get<any>(`${environment.baseAPI}/api/ProductAPI/getProductPage`,{params})

    //${environment.baseAPI}/api/ProductAPI/getProductPage?pageNum=${pageNum}&pageSize=${pageSize}
  }
  getProductById(id: string): Observable<any>{
    return this.http.get<any>(`${environment.baseAPI}/api/ProductAPI/${id}`)
  }

  getProductbyCategoryId(id: string): Observable<any>{
    return this.http.get<any>(`${environment.baseAPI}/api/ProductAPI/getPruductsByCategoryId/${id}`)
  }

  getallCategories(): Observable<any>{
    return this.http.get<any>(`${environment.baseAPI}/api/ProductAPI/getAllCategories`)
  }

    

}
