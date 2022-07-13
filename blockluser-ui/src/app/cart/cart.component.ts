import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Movie } from '../contacts/movie';
import { ApiService } from '../services/api-service.service';
import { RatingsMapper } from '../contacts/enums/ratings.enum';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss'],
})
export class CartComponent implements OnInit {
  cart: Movie[] = [];
  ratingsMapper = RatingsMapper;

  constructor(private apiService: ApiService, private router: Router) {}

  ngOnInit(): void {
    var token = localStorage['token'];

    if (!this.apiService.IsLoggedIn()) {
      this.router.navigateByUrl('/');
    } else {
      if (localStorage['cart']) {
        this.cart = this.apiService.Cart();
      }
    }
  }

  public removeFromCart(id: string) {
    const index = this.cart.findIndex((movie) => {
      return movie.Id === id;
    });
    this.cart.splice(index, 1);
    localStorage['cart'] = JSON.stringify(this.cart);
  }

  public async checkOut() {
    await this.apiService.CheckOutMovies(this.cart);
    this.cart = [];
    localStorage.removeItem('cart');
    location.href = '/';
  }
}
