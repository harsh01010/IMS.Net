import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { bootstrapSearch } from '@ng-icons/bootstrap-icons';
import { NgIconComponent, provideIcons } from '@ng-icons/core';
import { RouterLink, Router, ActivatedRoute, Event, NavigationEnd } from '@angular/router';

import { Location } from '@angular/common';
import { NavSearchComponent } from "./nav-search/nav-search.component";
@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterLink, NgIconComponent, NavSearchComponent],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
  viewProviders: [provideIcons({ bootstrapSearch })]
})
export class NavbarComponent implements OnInit {
  isDropdownOpen = false;
  currentRoute = '';
  showSearchComponent = false;
  constructor(private router: Router, private location: Location) { }
  ngOnInit(): void {
    this.router.events.subscribe((event: Event) => {
      if (event instanceof NavigationEnd) {
        this.currentRoute = (<NavigationEnd>event).url;
        console.log(this.currentRoute);
      }
    })
  }


  setShowSearchComponent = () => { this.showSearchComponent = true; }
  unsetShowSearchComponent = () => { this.showSearchComponent = false; }

  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen;
  }
}
