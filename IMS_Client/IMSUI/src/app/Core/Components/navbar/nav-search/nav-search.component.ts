import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Product } from '../../../../Models/Product.model';
import { ProductService } from '../../../../Services/Product/product.service';
import { FormsModule } from '@angular/forms';
import { LoaderComponent } from "../../reuseable/loader/loader.component";
import { ProductCardSmallComponent } from "../../reuseable/product-card-small/product-card-small.component";

@Component({
  selector: 'app-nav-search',
  standalone: true,
  imports: [FormsModule, CommonModule, LoaderComponent, ProductCardSmallComponent],
  templateUrl: './nav-search.component.html',
  styleUrl: './nav-search.component.scss'
})
export class NavSearchComponent implements OnInit {

  constructor(private productService: ProductService) { }

  @Output() close = new EventEmitter<void>();
  seachResult!: Array<Product>
  allProducts!: Array<Product>
  searchQuery = ''
  loading = false


  ngOnInit(): void {
    this.fetchAllProducts();
  }

  fetchAllProducts = () => {
    this.loading = true
    this.productService.getallProducts().subscribe(
      {
        next: (res) => {
          if (res.isSuccess) {
            this.allProducts = res.result
            this.loading = false
          }
        },
        error: (err) => { console.log(err); }

      }
    )

  }

  searchThequery = () => {
    this.seachResult = this.allProducts.filter(x => x.name.toLocaleLowerCase().includes(this.searchQuery.toLocaleLowerCase()));
  }

  closeComponent = () => {
    this.close.emit();
  }


}
