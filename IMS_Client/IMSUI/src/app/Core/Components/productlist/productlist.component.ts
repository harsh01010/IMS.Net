import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../Services/Product/product.service';
import { Product } from '../../../Models/Product.model';
import { CommonModule } from '@angular/common';
import { LoaderComponent } from "../reuseable/loader/loader.component";

@Component({
  selector: 'app-productlist',
  standalone: true,
  imports: [CommonModule, LoaderComponent],
  templateUrl: './productlist.component.html',
  styleUrl: './productlist.component.scss'
})
export class ProductlistComponent implements OnInit {
  constructor(private productService: ProductService){
  }

  pageNumber = 1
  pageSize= 10
  showLoadMore=true
  loading=false

  products?: Array<Product>;
  ngOnInit(): void {
    /*
    this.productService.getallProducts().subscribe({
      next: (products) => {
        this.products = products.result;  
        console.log(products.result);
      },
      error: (err) => {
        console.error(err);
      }

    })
    */
   this.loading   = true;
    this.productService.getProductPage( this.pageNumber,this.pageSize).subscribe({
        next:(response) =>{
          this.loading = false;
            if(response.isSuccess===true)
              {
                this.products = response.result;
                //this.products = this.products!=null?[...this.products,...response.result]:response.result;
              }
            else{
              this.showLoadMore = response!=null && response.result.length === 0 ? false:true; 
            }
        }
    })

  }

  fetchNextPage() {
    this.loading = true;
    this.pageNumber += 1;
    this.productService.getProductPage( this.pageNumber,this.pageSize).subscribe({
      next:(response) =>{
        this.loading = false;
          if(response.isSuccess===true)
            {
              this.products = this.products!=null?[...this.products,...response.result]:response.result;
            }
          else{
            this.showLoadMore = response!=null && response.result.length === 0 ? false:true; 
          }
      }
  })

  }

}
