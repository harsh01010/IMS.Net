import { Component, OnInit } from '@angular/core';
import { CartService } from '../../../Services/Cart/cart.service';
import { OrderService } from '../../../Services/Order/order.service';
import { AddressService } from '../../../Services/Order/address.service';
import { Address } from '../../../Models/Address.model';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TokenStorageService } from '../../../Services/token/token.service';



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
  public cartId!:string ;

  // userId:string="cb48ba81-67fe-4e04-9fcb-8c5411080dee";

  constructor(
    private orderService: OrderService,
    private addressService: AddressService,
    private cartService: CartService,
    private router: Router,
    private tokenService: TokenStorageService

  ) {}

  ngOnInit(): void {
    this.cartId=this.tokenService.getUser().id;
    this.fetchAddresses();
  }

  fetchAddresses() {
    this.addressService.getUserAddresses( this.cartId).subscribe({
      next: (addresses) => {
        console.log(addresses.result);
        // Assuming `addresses.result` is an array with `shippingId` in each address object
        this.addresses = addresses.result.map((address: any) => ({
          shippingId: address.shippingAddressId, // Store shippingId
          houseNo: address.houseNo,
          street: address.street,
          pinCode: address.pinCode,
          city: address.city,
          state: address.state
        }));
        console.log(this.addresses); // You can now see shippingId with each address
       
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
    this.addressService.addAddress(this.newAddress,  this.cartId).subscribe({
      next: (response) => {
        alert('Address added successfully:');

        console.log(response)
        this.fetchAddresses(); // Refresh the address list after adding a new one
        this.showAddAddress = false; // Hide the add address form
      },
      error: (error) => console.error('Error adding address:', error)
    });
  }
  deleteAddress(address: Address) {
    if (address.shippingId) {
      this.addressService.deleteAddress(address.shippingId).subscribe({
        next: (response) => {
        alert('Address deleted successfully:');

          console.log('Address deleted successfully:', response);
          this.fetchAddresses(); // Refresh the address list after deleting
        },
        error: (error) => console.error('Error deleting address:', error)
      });
    } else {
      console.error('Cannot delete address: Shipping ID is missing.');
    }
  }
  

  confirmOrder() {
    if (!this.selectedAddress) {
      alert('Please select or add a delivery address.');
      return;
    }

    this.orderService.placeOrder(this.selectedAddress.shippingId, this.cartId).subscribe({
      next: (response) => {
        // this.orderSuccess = true;
        alert("Order Placed Succesfully")
     this.deleteCart();
        this.router.navigate([''])
        
      },
      error: (error) => console.error('Error placing order:', error)

    });

  }

  deleteCart = ()=>{
    this.cartService.deleteCart( this.cartId).subscribe({
      next: (res) => {
        console.log(res);
      },
      error: (err) => {
        console.error('Error deleting cart', err);
      }
    });
  }
}
