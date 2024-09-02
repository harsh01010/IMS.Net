import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TokenStorageService } from '../token/token.service';

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService implements HttpInterceptor {

  constructor(private sesssion:TokenStorageService) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
  const token = this.sesssion.getToken();
  console.log("Token retrieved:", token);

  if (token) {
    const clonedReq = req.clone({
      headers: req.headers.set('Authorization', `Bearer ${token}`)
    });
    console.log("Authorization header added:", clonedReq.headers.get('Authorization'));
    console.log(clonedReq); // Add this line
    return next.handle(clonedReq);
  } else {
    console.log("No token found, sending request without Authorization header"); // Add this line
    return next.handle(req);
  }
}

}
