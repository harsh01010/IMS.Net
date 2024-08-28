import { Component, OnInit } from '@angular/core';
import { ReturnProductFromCartDto } from '../../../Models/ReturnProductFromCart.model';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { BehaviorSubject } from 'rxjs';
import { CartService } from '../../../Services/Cart/cart.service';
import { CartProduct } from '../../../Models/DeleteProductFromCart.model';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  public grandTotal!: number;
  private cartTotal = new BehaviorSubject<number>(0);
  public cartItemList = new BehaviorSubject<ReturnProductFromCartDto[]>([]);
  userId: string = 'cb48ba81-67fe-4e04-9fcb-8c5411080dee';
  cartProduct: CartProduct = { productId: '' };

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    // Fetch items from the cart service
    this.cartService.getProductById(this.userId).subscribe({
      next: (response) => {
        const cartItems = response.result.products;
        // console.log(response.result);
        this.cartItemList.next(cartItems);
        this.updateCartTotal();
      },
      error: (error) => {
        console.error('Error fetching cart items:', error);
        this.cartItemList.next([]);
        this.updateCartTotal();
      }
    });

    // Optionally, subscribe to cartItemList to update UI reactively
    this.cartItemList.subscribe(items => {
      this.grandTotal = this.getTotalPrice();
    });
  }

  removeCartItem(cartItemId: string) {
    // Assign the cartItemId to the cartProduct object
    this.cartProduct.productId = cartItemId;

    // Call the service to delete the product by ID
    this.cartService.deleteProductFromCartById(this.userId, this.cartProduct).subscribe({
      next: () => {
        // On success, remove the item from the cart list
        const currentItems = this.cartItemList.value;
        const updatedItems = currentItems.filter(cartItem => cartItem.productId !== cartItemId);

        // Update the cartItemList with the filtered items
        this.cartItemList.next(updatedItems);

        // Update the total price
        this.updateCartTotal();

        console.log('Product deleted successfully and cart updated.');
      },
      error: (error) => {
        console.error('Error deleting product:', error);
      }
    });
  }

  removeAllCart() {
    this.cartService.deleteCart(this.userId).subscribe({
      next: () => {
        this.cartItemList.next([]);
        this.cartTotal.next(0);
      }
    });
  }

  // Function to get the total price of items in the cart
  getTotalPrice(): number {
    const products = this.cartItemList.value;
    const totalPrice = products.reduce((sum, item) => sum + item.price * item.productCount, 0);
    return totalPrice;
  }

  // Update total price based on the items in the cart
  private updateCartTotal() {
    const total = this.getTotalPrice();
    this.cartTotal.next(total);
  }
}