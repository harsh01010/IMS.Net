import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { ProductService } from '../../../../Services/Product/product.service';
import { Product } from '../../../../Models/Product.model';
import { error } from 'console';
import { CommonModule } from '@angular/common';
import { ProductCardBigComponent } from "../../reuseable/product-card-big/product-card-big.component";
import { RouterLink } from '@angular/router';
import { LoaderComponent } from "../../reuseable/loader/loader.component";


@Component({
  selector: 'app-category-products',
  standalone: true,
  imports: [CommonModule, ProductCardBigComponent, RouterLink, LoaderComponent],
  templateUrl: './category-products.component.html',
  styleUrl: './category-products.component.scss'
})
export class CategoryProductsComponent implements OnInit {
  @Input() catId!: string;

  @Output() close = new EventEmitter<void>();
  loading = false;

  constructor(private productService: ProductService) { }

  products!: Array<Product>

  ngOnInit(): void {
    this.fetchProductsByCat();
  }

  fetchProductsByCat = () => {
    this.loading = true;

    this.productService.getProductbyCategoryId(this.catId).subscribe(
      {
        next: (res) => {
          if (res.isSuccess) {
            this.products = res.result;
            console.log(this.products);
          }
        },
        error: (err) => { console.log(err); }
      }
    )
    this.loading = false;
  }


  closeComponent = () => {
    this.close.emit();
  }

}
