import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { NavbarComponent } from "./Core/Components/navbar/navbar.component";
import { CommonModule } from '@angular/common';
import { ProductlistComponent } from "./Core/Components/productlist/productlist.component";
import { LoginComponent } from './Core/Components/login/login.component';
import { RegisterComponent } from './Core/Components/register/register.component';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavbarComponent, CommonModule, ProductlistComponent,RouterModule, LoginComponent, RegisterComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'IMSUI';
}
