import { CommonModule, NgForOfContext } from '@angular/common';
import { Component, OnDestroy } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { Product } from '../../../Models/Product.model';
import { AddNewProduct } from '../../../Models/Add-Product.model';
import { ProductService } from '../../../Services/Product/product.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-add-new-product',
  standalone: true,
  imports: [RouterLink,CommonModule,FormsModule],
  templateUrl: './add-new-product.component.html',
  styleUrl: './add-new-product.component.scss'
})
export class AddNewProductComponent implements OnDestroy {
 product: AddNewProduct = {
    name: '',
    price: 0,
    description: '',
    availableQuantity:0,
    categoryID: '',
    imageUrl: '',
    imageLocalPath: '',
  };
  constructor(private productservice: ProductService){

  }
  subsciption?:Subscription

  onSubmit(form:NgForm){
    console.log(this.product);
    if(form.valid){
   this.subsciption=this.productservice.addProduct(this.product).subscribe({
    next: (response) => {
      console.log(response);
      form.resetForm();
    },
    error: (err) => {
      console.error(err);
    }  })};
   }
    ngOnDestroy(): void {
    if(this.subsciption){
      this.subsciption.unsubscribe();
    }
  }

  
}
