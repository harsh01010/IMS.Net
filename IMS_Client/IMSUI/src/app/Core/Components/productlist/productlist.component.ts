import { Component, OnDestroy, OnInit } from '@angular/core';
import { ProductService } from '../../../Services/Product/product.service';
import { Product } from '../../../Models/Product.model';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Subscription } from 'rxjs';
import { CartService } from '../../../Services/Cart/cart.service';

@Component({
  selector: 'app-productlist',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './productlist.component.html',
  styleUrls: ['./productlist.component.scss']
})
export class ProductlistComponent implements OnInit, OnDestroy {
  products?: Product[];
  subscription?: Subscription;

  constructor(private productService: ProductService, private cartService: CartService) {}

  ngOnInit(): void {
    this.subscription = this.productService.getallProducts().subscribe({
      next: (response) => {
   
        this.products = response.result;
        console.log(response.result);
        console.log(this.products);
      },
      error: (err) => {
        console.error(err);
      }
    });
  }

  

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
}
