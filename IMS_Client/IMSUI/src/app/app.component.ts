import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { NavbarComponent } from "./Core/Components/navbar/navbar.component";
import { CommonModule } from '@angular/common';
import { ProductlistComponent } from "./Core/Components/productlist/productlist.component";
import { LoginComponent } from './Core/Components/login/login.component';
import { RegisterComponent } from './Core/Components/register/register.component';
import { FormsModule } from '@angular/forms';
import { NgIconsModule } from '@ng-icons/core';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthServiceService } from './Services/Auth/auth-service.service';





@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavbarComponent, CommonModule, ProductlistComponent,RouterModule,FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  providers:[{provide:HTTP_INTERCEPTORS,useClass:AuthServiceService,multi:true}]
})
export class AppComponent {
  title = 'IMSUI';
}
