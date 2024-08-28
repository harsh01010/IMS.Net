import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ProductService } from '../../../Services/Product/product.service';
import { Product } from '../../../Models/Product.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-product-dashboard',
  standalone: true,
  imports: [CommonModule,RouterLink],
  templateUrl: './product-dashboard.component.html',
  styleUrl: './product-dashboard.component.scss'
})
export class ProductDashboardComponent implements OnInit, OnDestroy {

  constructor(private productService: ProductService){

  }

  
  products?: Product[];
  subscription?: Subscription;
  ngOnInit(): void {
    this.subscription=this.productService.getallProducts().subscribe({
      next: (response) => {
        this.products = response.result;  
        console.log(this.products);
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

  deleteProduct(productId: string): void {
    this.productService.deleteproduct(productId).subscribe({
      next: () => {
        console.log('Product deleted successfully');
        this.products = this.products?.filter(p => p.productId!== productId);
      },
      error: (err) => {
        console.error('Error deleting product', err);
      }
    });
  }


}
