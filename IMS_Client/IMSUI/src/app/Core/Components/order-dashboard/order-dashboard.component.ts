import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../../Services/Order/order.service';
import { Subscription } from 'rxjs';
import { order } from '../../../Models/order.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-order-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './order-dashboard.component.html',
  styleUrl: './order-dashboard.component.scss'
})
export class OrderDashboardComponent implements OnInit {

  constructor( private orderservice:OrderService) { }
  orders:order[]=[]
  Subscription ?:Subscription
  ngOnInit(): void {
  this.Subscription = this.orderservice.getallOrders().subscribe({
    next: (response) => {
      console.log(response);
      this.orders = response.result;
    },
    error: (error) => {
      console.log(error);
    }
  })
    
  }



}
