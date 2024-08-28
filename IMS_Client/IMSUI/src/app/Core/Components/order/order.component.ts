import { Component, OnInit } from '@angular/core';
import { CartService } from '../../../Services/Cart/cart.service';
import { OrderService } from '../../../Services/Order/order.service';
import { AddressService } from '../../../Services/Order/address.service';
import { Address } from '../../../Models/Address.model';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';



@Component({
  selector: 'app-order',
  standalone: true,
  imports:  [CommonModule,FormsModule],
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent implements OnInit {
  addresses: Address[] = [];
  selectedAddress: Address | null = null;
  newAddress: Address = {  houseNo : '' ,street: '',pinCode: '', city: '', state:'' };
  showAddAddress = false;
  orderSuccess = false;
  userId:string="cb48ba81-67fe-4e04-9fcb-8c5411080dee";

  constructor(
    private orderService: OrderService,
    private addressService: AddressService,
    private cartService: CartService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.fetchAddresses();
  }

  fetchAddresses() {
    this.addressService.getUserAddresses(this.userId).subscribe({
      next: (addresses) => {
        this.addresses = addresses.result;
        console.log(addresses);

      },
      error: (error) => console.error('Error fetching addresses:', error)
    });
  }

  toggleAddAddress() {
    this.showAddAddress = !this.showAddAddress;
  }

  addAddress() {
    // Check for redundancy before adding a new address
    const isDuplicate = this.addresses.some(existingAddress =>
      existingAddress.houseNo === this.newAddress.houseNo &&
      existingAddress.street === this.newAddress.street &&
      existingAddress.city === this.newAddress.city &&
      existingAddress.pinCode === this.newAddress.pinCode &&
      existingAddress.state === this.newAddress.state
    );
  
    if (isDuplicate) {
      console.error('Address already exists.');
      // Optionally show a message to the user
      alert('This address already exists. Please enter a different address.');
      return; 
    }
  
    // If not a duplicate, proceed to add the address
    this.addressService.addAddress(this.newAddress, this.userId).subscribe({
      next: (response) => {
        this.fetchAddresses(); // Refresh the address list after adding a new one
        this.showAddAddress = false; // Hide the add address form
      },
      error: (error) => console.error('Error adding address:', error)
    });
  }
  

  confirmOrder() {
    if (!this.selectedAddress) {
      alert('Please select or add a delivery address.');
      return;
    }

    this.orderService.placeOrder(this.selectedAddress).subscribe({
      next: (response) => {
        this.orderSuccess = true;
        this.cartService.deleteCart(this.userId) // Clear cart
        setTimeout(() => this.router.navigate(['']), 3000); // Redirect to home after 3 seconds
      },
      error: (error) => console.error('Error placing order:', error)
    });
  }
}