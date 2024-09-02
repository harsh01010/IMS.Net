import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductService } from '../../../Services/Product/product.service';
import { category } from '../../../Models/Category.model';
import { bootstrapArrowRight } from '@ng-icons/bootstrap-icons';
import { NgIconComponent, provideIcons } from '@ng-icons/core';
import { CategoryProductsComponent } from "./category-products/category-products.component";
import { LoaderComponent } from "../reuseable/loader/loader.component";
@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [CommonModule, NgIconComponent, CategoryProductsComponent, LoaderComponent],
  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.scss',
  viewProviders: [provideIcons({ bootstrapArrowRight })]
})
export class CategoryListComponent implements OnInit {

  constructor(private productService: ProductService) { }

  categories?: any
  loading = false
  catId!: string
  productDisplayFlag = false
  ngOnInit(): void {

    this.getAllCategories();

  }


  getAllCategories = () => {
    this.loading = true;

    this.productService.getallCategories()
      .subscribe({
        next: (res) => {
          if (res.isSuccess) {
            this.categories = res.result;
            console.log(this.categories);
          }
        },
        error: (err) => {
          console.log(err);
        }
      })

    this.loading = false;
  }

  productDisplay = (id: string) => { this.catId = id; this.productDisplayFlag = true }
  closeProduct = () => { this.productDisplayFlag = false }
}
