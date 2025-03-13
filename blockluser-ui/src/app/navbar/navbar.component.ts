import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api-service.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  loggedIn: boolean = false;
  isAdmin: boolean = false;
  hasRentedMovies: boolean = false;
  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.loggedIn = this.apiService.IsLoggedIn();
    this.isAdmin = this.apiService.IsAdmin();
    if (this.loggedIn) {
      const rentedMovies = this.apiService.RentedMovies();
      this.hasRentedMovies = rentedMovies && rentedMovies.length > 0;
    }
  }

  Signout() {
    this.apiService.SignOut();
    this.loggedIn = false;
    this.isAdmin = false;
  }
}
