import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api-service.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  loggedIn: boolean = false;
  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.loggedIn = this.apiService.IsLoggedIn();
  }

  Signout() {
    this.apiService.SignOut();
    this.loggedIn = false;
  }
}
