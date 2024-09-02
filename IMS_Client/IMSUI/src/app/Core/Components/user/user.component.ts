import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../../Services/Order/order.service';
import { TokenStorageService } from '../../../Services/token/token.service';
import { order } from '../../../Models/order.model';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { LoaderComponent } from "../reuseable/loader/loader.component";
interface OrderItem {
  productId: string;
  name: string;
  price: number;
  imageUrl: string;
  categoryId: string;
}

interface Order {
  orderId: string;
  ordertime: string;
  orderValue: number;
  status: boolean;
  items: OrderItem[];
}
@Component({
  selector: 'app-user',
  standalone: true,
  imports: [CommonModule, RouterLink, LoaderComponent],
  templateUrl: './user.component.html',
  styleUrl: './user.component.scss'
})
export class UserComponent implements OnInit {

  constructor(private orderService: OrderService, private tokenService: TokenStorageService) { }
  orders:Order[]=[]
  loading = false
  ngOnInit(): void {
    this.fetchData()

  }

  fetchData = () => {
    var userId = this.tokenService.getUser().id;
    this.loading=true;
    var res = this.orderService.getOrderHistory(userId).subscribe({
      next: (data) => {
        this.orders = data.result;
        this.loading=false;
        console.log(this.orders);
      },
      error: (error) => {
        console.error(error);
      }
    });
  }

}
