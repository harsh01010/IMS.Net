import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../../Models/Product.model';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { AddNewProduct } from '../../Models/Add-Product.model';
import { category } from '../../Models/Category.model';
import { AddCategory } from '../../Models/AddCategory.model';
@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http:HttpClient) { }

  getallProducts(): Observable<any>{
    return this.http.get<any>(`${environment.baseAPI}/api/ProductAPI`)
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

  deleteproduct(id: string): Observable<any>{
    return this.http.delete<any>(`${environment.baseAPI}/api/ProductAPI/${id}`)
  }
  addProduct(product:AddNewProduct): Observable<AddNewProduct>{
    return this.http.post<AddNewProduct>(`${environment.baseAPI}/api/ProductAPI`, product)
  }
  updateProduct(id:string,product: any): Observable<any> {
  return this.http.put<any>(`${environment.baseAPI}/api/ProductAPI/${id}`, product);
}
deleteCategory(id:string): Observable<any> {
  return this.http.delete<any>(`${environment.baseAPI}/api/ProductAPI/deleteCategory/${id}`);
}
  addCategory(category:AddCategory): Observable<any> {
  return this.http.post<any>(`${environment.baseAPI}/api/ProductAPI/addNewCategory`, category);
}
}
