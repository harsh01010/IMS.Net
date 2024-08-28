import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { bootstrapSearch, bootstrapCart2, bootstrapPersonCircle } from '@ng-icons/bootstrap-icons';
import { NgIconComponent, provideIcons } from '@ng-icons/core';
import { RouterLink, Router, Event, NavigationEnd } from '@angular/router';

import { Location } from '@angular/common';
import { NavSearchComponent } from "./nav-search/nav-search.component";
import { TokenStorageService } from '../../../Services/token/token.service';
import { LoginTempComponent } from "../login-temp/login-temp.component";
import { RegisterComponent } from "../register/register.component";
@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterLink, NgIconComponent, NavSearchComponent, LoginTempComponent, RegisterComponent],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
  viewProviders: [provideIcons({ bootstrapSearch, bootstrapCart2, bootstrapPersonCircle })]
})
export class NavbarComponent implements OnInit {

  constructor(private router: Router, private location: Location, private sessionStorageServie: TokenStorageService) { }
  isDropdownOpen = false;
  currentRoute = '';
  showSearchComponent = false;
  userLogedIn = false;
  showlogin = false;
  showRegister = false;
  isAdmin = false;
  userId!: string


  ngOnInit(): void {
    this.userLogedIn = this.sessionStorageServie.getToken() != null;
    this.userId = this.sessionStorageServie.getUser() != null ? this.sessionStorageServie.getUser().id : '';
    this.isAdmin = this.sessionStorageServie.getToken() != null ? this.sessionStorageServie.getUser().role == "Admin" : false;
    this.router.events.subscribe((event: Event) => {
      if (event instanceof NavigationEnd) {
        this.currentRoute = (<NavigationEnd>event).url;
        console.log(this.currentRoute);
      }
    })
    this.sessionStorageServie.getToken() != null;
  }


  setShowSearchComponent = () => { this.showSearchComponent = true; }
  unsetShowSearchComponent = () => { this.showSearchComponent = false; }

  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  /* login/register rendering logic*/
  toggleShowLogin = () => {
    this.showlogin = !this.showlogin;
    this.showRegister = false;
    console.log(this.showlogin, this.showRegister)

  }

  closeLogin() {
    this.showlogin = false;
    //console.log(this.showlogin,this.showRegister)
  }
  closeRegister() {
    this.showRegister = false;
    //console.log(this.showlogin,this.showRegister)
  }
  toggleShowRegister = () => {
    this.showRegister = !this.showRegister;
    this.showlogin = false;
    console.log(this.showlogin, this.showRegister)

  }

  //set user login
  loginStatus = (status: boolean) => {
    this.userLogedIn = status;
    this.showlogin = !status;
    this.isDropdownOpen = !status;
    this.userId = this.sessionStorageServie.getUser() != null ? this.sessionStorageServie.getUser().id : '';
    this.isAdmin = this.sessionStorageServie.getToken() != null ? this.sessionStorageServie.getUser().role == "Admin" : false;
  }


  //logout
  logout = () => {
    this.sessionStorageServie.signOut();
    this.userLogedIn = false
    this.isDropdownOpen = !this.isDropdownOpen
    this.userId = '';
    this.isAdmin = false;
  }


}
