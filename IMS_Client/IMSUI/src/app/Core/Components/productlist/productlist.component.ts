import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ProductService } from '../../../Services/Product/product.service';
import { Product } from '../../../Models/Product.model';
import { CommonModule } from '@angular/common';
import { LoaderComponent } from "../reuseable/loader/loader.component";
import { RouterLink } from '@angular/router';
import { ProductCardBigComponent } from "../reuseable/product-card-big/product-card-big.component";
import { CategoryListComponent } from "../category-list/category-list.component";

@Component({
  selector: 'app-productlist',
  standalone: true,
  imports: [CommonModule, LoaderComponent, RouterLink, ProductCardBigComponent, CategoryListComponent],
  templateUrl: './productlist.component.html',
  styleUrls: ['./productlist.component.scss']
})
export class ProductlistComponent implements OnInit {
  @ViewChild('loadMoreBtn') loadMoreBtn!: ElementRef<HTMLButtonElement>;

  constructor(private productService: ProductService) {}

  pageNumber = 1;
  pageSize = 12;
  showLoadMore = true;
  loading = false;

  products: Array<Product> = [];

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.loading = true;
    this.productService.getProductPage(this.pageNumber, this.pageSize).subscribe({
      next: (response) => {
        this.loading = false;
        if (response.isSuccess) {
          this.products = [...this.products, ...response.result];
          this.showLoadMore = response.result.length === this.pageSize;
          this.scrollToLoadMoreBtn();
        } else {
          this.showLoadMore = false;
        }
      },
      error: () => {
        this.loading = false;
        this.showLoadMore = false;
      }
    });
  }

  fetchNextPage(): void {
    if (this.showLoadMore) {
      this.pageNumber += 1;
      this.loadProducts();
    }
  }

  trackByProductId(index: number, product: Product): string {
    return product.productId; 
  }
  private scrollToLoadMoreBtn(): void {
    if (this.loadMoreBtn) {
      this.loadMoreBtn.nativeElement.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
  }
}
