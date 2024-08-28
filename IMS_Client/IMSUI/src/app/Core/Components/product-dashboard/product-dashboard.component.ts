import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ProductService } from '../../../Services/Product/product.service';
import { Product } from '../../../Models/Product.model';
import { Subscription } from 'rxjs';
import { LoaderComponent } from '../reuseable/loader/loader.component';
@Component({
  selector: 'app-product-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, LoaderComponent],
  templateUrl: './product-dashboard.component.html',
  styleUrl: './product-dashboard.component.scss'
})
export class ProductDashboardComponent implements OnInit, OnDestroy {

  constructor(private productService: ProductService){

  }

  
  products?: Product[];
  subscription?: Subscription;
  loading=false;
  ngOnInit(): void {
    this.loading=true;
    this.subscription=this.productService.getallProducts().subscribe({
      next: (response) => {
        this.loading=false;
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
