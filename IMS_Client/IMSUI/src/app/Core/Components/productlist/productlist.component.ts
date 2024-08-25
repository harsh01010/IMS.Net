import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../Services/Product/product.service';
import { Product } from '../../../Models/Product.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-productlist',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './productlist.component.html',
  styleUrl: './productlist.component.scss'
})
export class ProductlistComponent implements OnInit {
  constructor(private productService: ProductService){
  }
  products?: any;
  ngOnInit(): void {
    this.productService.getallProducts().subscribe({
      next: (products) => {
        this.products = products.result;  
        console.log(products.result);
      },
      error: (err) => {
        console.error(err);
      }

    })
  }

}
