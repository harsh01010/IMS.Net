import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Subscription } from 'rxjs';
import { ProductService } from '../../../Services/Product/product.service';
import { Product } from '../../../Models/Product.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [CommonModule,RouterLink],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit,OnDestroy  {
   constructor(private route:ActivatedRoute,private productService:ProductService){

   }
   subsciption?:Subscription
   productId?:string|null
   product?:Product
   categoryID?:string|null

   CategorisedProduct?:Product[]
  ngOnInit(): void {
    this.subsciption = this.route.paramMap.subscribe( {
      next:(params)=>{
        this.productId = params.get('productId');
        console.log(this.productId);
        if(this.productId){
        this.productService.getProductById(this.productId).subscribe({
          next:(response)=>{
          this.product = response.result;
          console.log(this.product);
          this.categoryID =response.result.categoryID;
           if(this.categoryID){
             this.productService.getProductbyCategoryId(this.categoryID).subscribe({
              next:(response)=>{
               this.CategorisedProduct = response.result;
               console.log(this.CategorisedProduct)
              }
             })
           }
          }
        })
      }
      }
    });
    
  }
  
  ngOnDestroy(): void {
    if(this.subsciption){
      this.subsciption.unsubscribe();
    }
  }
}
