
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Subscription } from 'rxjs';
import { ProductService } from '../../../Services/Product/product.service';
import { Product } from '../../../Models/Product.model';
import { CommonModule } from '@angular/common';
import { ProductCardBigComponent } from "../reuseable/product-card-big/product-card-big.component";
import { LoaderComponent } from "../reuseable/loader/loader.component";
import { CartService } from '../../../Services/Cart/cart.service';
import { Router } from '@angular/router';
import { TokenStorageService } from '../../../Services/token/token.service';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [CommonModule, RouterLink, ProductCardBigComponent, LoaderComponent],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit, OnDestroy {
  constructor(private route: ActivatedRoute,private router: Router , private productService: ProductService, private cartService: CartService,  private tokenService: TokenStorageService) {

  }
 cartId !:string
  subsciption?: Subscription
  productId?: string | null
  product?: Product
  categoryID?: string | null
  loadingDetails = false
  loadingProducts = false
  CategorisedProduct?: Product[]
  // cart!:string;
  ngOnInit(): void {
    this.cartId=this.tokenService.getUser().id;
    console.log(this.cartId)

    this.subsciption = this.route.paramMap.subscribe({
      next: (params) => {
        this.productId = params.get('productId');
        console.log(this.productId);
        this.fetchAll();
      }
    });




  }

  fetchAll = () => {
    this.loadingDetails = true;
    if (this.productId) {

      this.productService.getProductById(this.productId).subscribe({
        next: (response) => {
          this.product = response.result;
          //console.log(this.product);
          this.loadingDetails = false
          this.categoryID = response.result.categoryID;
          this.fetchProducts();
        }
      })
    }
  }

  fetchProducts = () => {
    this.loadingProducts = true;
    if (this.categoryID) {
      this.productService.getProductbyCategoryId(this.categoryID).subscribe({
        next: (response) => {
          this.CategorisedProduct = (response.result as Product[]).filter(x => x.productId !== this.productId);
          this.loadingProducts = false
          console.log(this.CategorisedProduct)
        }
      })
    }
  }
  addProductToCart(  ){
    this.cartService.addProductToCart(this.cartId, this.productId).subscribe({
      next: () => {
      
        console.log('Product added to cart successfully');
        this.router.navigate(['/api/cart']);
      },
      error: (err) => {
        console.error('Error adding product to cart', err);
      } 
    }); 

  }
  ngOnDestroy(): void {
    if (this.subsciption) {
      this.subsciption.unsubscribe();
    }
  }
}
