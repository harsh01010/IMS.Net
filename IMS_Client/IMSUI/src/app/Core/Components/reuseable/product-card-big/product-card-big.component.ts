import { Component, Input } from '@angular/core';
import { RouterLink, RouterModule } from '@angular/router';
import { Product } from '../../../../Models/Product.model';

@Component({
  selector: 'app-product-card-big',
  standalone: true,
  imports: [RouterLink, RouterModule],
  templateUrl: './product-card-big.component.html',
  styleUrl: './product-card-big.component.scss'
})
export class ProductCardBigComponent {

  @Input() product!: Product

}
