import { Component } from '@angular/core';
import { AddNewProduct } from '../../../Models/Add-Product.model';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ProductService } from '../../../Services/Product/product.service';
import { FormsModule} from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-edit-product',
  standalone: true,
  imports: [RouterLink,FormsModule,CommonModule],
  templateUrl: './edit-product.component.html',
  styleUrl: './edit-product.component.scss'
})
export class EditProductComponent {
product: AddNewProduct = {
    name: '',
    price: 0,
    description: '',
    availableQuantity: 0,
    categoryID: '',
    imageUrl: '',
    imageLocalPath: '',
  };

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private router: Router
  ) {}
  productId?:string|null;
  ngOnInit(): void {
     this.productId= this.route.snapshot.paramMap.get('productId');
     console.log(this.productId)
    if (this.productId) {
      this.productService.getProductById(this.productId).subscribe({

      next: (data) => {
        this.product = data.result;  
        console.log(this.product);
      },
      error: (err) => {
        console.error(err);
      }
      }
    );
    }
  }

  onUpdate() {
    if (this.productId) {
    this.productService.updateProduct(this.productId,this.product).subscribe({
      next:(response) => {
        console.log('Product updated successfully',response);
        this.router.navigate(['/api/ProductAPI']);
      },
      error:(error) => {
        console.error('Error updating product', error);
      }

    }
    );
  }
}
}
