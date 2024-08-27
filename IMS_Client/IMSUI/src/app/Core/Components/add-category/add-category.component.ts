import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Form, FormsModule, NgForm } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { ProductService } from '../../../Services/Product/product.service';
import { AddCategory } from '../../../Models/AddCategory.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-add-category',
  standalone: true,
  imports: [FormsModule,CommonModule,RouterLink],
  templateUrl: './add-category.component.html',
  styleUrl: './add-category.component.scss'
})
export class AddCategoryComponent implements OnInit,OnDestroy {

  constructor(private productService: ProductService){
  }

  Category:AddCategory={
    categoryName:''
  }

  subscriptions?:Subscription;

  ngOnInit(): void {
  }
   onSubmit(forms:NgForm){
    this.subscriptions = this.productService.addCategory(this.Category).subscribe({
      next: (response) => {
        console.log(response);
        forms.resetForm();

      },
      error: (err) => {
        console.error(err);
      }
    })
   }
   ngOnDestroy(): void {
    if(this.subscriptions){
      this.subscriptions.unsubscribe();
    }
    
  }




}
