import { Component, OnInit } from '@angular/core';
import { OrderService } from '../../../Services/Order/order.service';
import { TokenStorageService } from '../../../Services/token/token.service';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [],
  templateUrl: './user.component.html',
  styleUrl: './user.component.scss'
})
export class UserComponent implements OnInit {

  constructor(private orderService: OrderService, private tokenService: TokenStorageService) { }
  ngOnInit(): void {
  }

  fetchData = () => {
    var userId = this.tokenService.getUser().id;
    var res = this.orderService.getOrderHistory(userId);
  }

}
