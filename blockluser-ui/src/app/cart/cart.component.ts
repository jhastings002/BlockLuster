import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss'],
})
export class CartComponent implements OnInit {
  removed: any[] = [];
  cart: any[] = [];
  constructor(private router: Router) {}

  ngOnInit(): void {
    var token = localStorage['token'];
    console.log(token);

    if (token === 'undefined') {
      this.router.navigateByUrl('/');
      console.log('logged out');
    } else {
      this.cart = JSON.parse(localStorage['cart']);
    }
    console.log(this.cart);
  }

  public removeFromCart(id: string) {
    this.removed.push(
      this.cart.find((movie: { Id: string }) => {
        return movie.Id === id;
      })
    );
    const index = this.cart.findIndex((movie: { Id: string }) => {
      return movie.Id === id;
    });
    this.cart.splice(index, 1);
    localStorage['cart'] = JSON.stringify(this.cart);
    localStorage['removed'] = this.removed;
  }
}
