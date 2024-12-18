import { Component, OnDestroy, OnInit } from '@angular/core';
import { category } from '../../../Models/Category.model';
import { Subscription } from 'rxjs';
import { ProductService } from '../../../Services/Product/product.service';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { LoaderComponent } from "../reuseable/loader/loader.component";

@Component({
  selector: 'app-category-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, LoaderComponent],
  templateUrl: './category-dashboard.component.html',
  styleUrl: './category-dashboard.component.scss'
})
export class CategoryDashboardComponent implements OnInit, OnDestroy {

  constructor(private productService: ProductService){
  }

  categories?: any;
  subscription?: Subscription;
  loading=false;
  ngOnInit(): void {
    this.loading=true;
    this.subscription=this.productService.getallCategories().subscribe({
      next: (response) => {
        this.loading=false;  //Loading is set to false once data is fetched successfully.
        this.categories = response.result;  
        console.log(this.categories)
      },
      error: (err) => {
        console.error(err);
      }

    })
  }
 
  OnSubmit(id: string){
    this.productService.deleteCategory(id).subscribe({
      next: (response) => {
        console.log(response);
      },
      error: (err) => {
        console.error(err);
      }
    });
  }
     ngOnDestroy(): void {
    if(this.subscription){
      this.subscription.unsubscribe();
    }
  }


}
