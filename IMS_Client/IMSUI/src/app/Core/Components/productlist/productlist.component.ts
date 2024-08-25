import { Component, OnDestroy, OnInit } from '@angular/core';
import { ProductService } from '../../../Services/Product/product.service';
import { Product } from '../../../Models/Product.model';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-productlist',
  standalone: true,
  imports: [CommonModule,RouterLink],
  templateUrl: './productlist.component.html',
  styleUrl: './productlist.component.scss'
})
export class ProductlistComponent implements OnInit,OnDestroy {
  constructor(private productService: ProductService){
  }

  products?: Product[];
  subscription?: Subscription;
  ngOnInit(): void {
    this.subscription=this.productService.getallProducts().subscribe({
      next: (response) => {
        this.products = response.result;  
      },
      error: (err) => {
        console.error(err);
      }

    })
  }
    ngOnDestroy(): void {
    if(this.subscription){
      this.subscription.unsubscribe();
    }
  }

}
