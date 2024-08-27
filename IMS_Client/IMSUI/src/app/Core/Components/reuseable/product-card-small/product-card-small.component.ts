import { Component, Input } from '@angular/core';
import { Product } from '../../../../Models/Product.model';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-product-card-small',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './product-card-small.component.html',
  styleUrl: './product-card-small.component.scss'
})
export class ProductCardSmallComponent {

  @Input() product!: Product;


}
