import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { NavbarComponent } from "./Core/Components/navbar/navbar.component";
import { CommonModule } from '@angular/common';
import { ProductlistComponent } from "./Core/Components/productlist/productlist.component";


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavbarComponent, CommonModule, ProductlistComponent,RouterModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'IMSUI';
}
