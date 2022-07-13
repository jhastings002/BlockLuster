import { Component, OnInit } from '@angular/core';
import { RatingsMapper } from '../contacts/enums/ratings.enum';
import { Movie } from '../contacts/movie';
import { ApiService } from '../services/api-service.service';

@Component({
  selector: 'app-movie-catalog',
  templateUrl: './movie-catalog.component.html',
  styleUrls: ['./movie-catalog.component.scss'],
})
export class MovieCatalogComponent implements OnInit {
  movies: Movie[] = [];
  cart: Movie[] = [];
  isLoggedIn: boolean = false;
  isAdmin: boolean = false;
  ratingMapper = RatingsMapper;

  constructor(private apiService: ApiService) {}

  async ngOnInit(): Promise<void> {
    this.isLoggedIn = this.apiService.IsLoggedIn();
    this.isAdmin = this.apiService.IsAdmin();
    this.movies = await this.apiService.GetCatalog();

    //remove movies in cart
    if (localStorage['cart']) {
      this.cart = JSON.parse(localStorage['cart']);
    }
    for (let movie of this.cart) {
      const foundIndex = this.movies.findIndex((x) => x.Id === movie.Id);
      if (foundIndex > -1) {
        this.movies.splice(foundIndex, 1);
      }
    }
  }

  public addToCart(id?: string) {
    const movie = this.movies.find((x) => {
      return x.Id === id;
    });

    if (movie) {
      this.cart.push(movie);
      const index = this.movies.findIndex((movie) => {
        return movie.Id === id;
      });
      this.movies.splice(index, 1);
      localStorage['cart'] = JSON.stringify(this.cart);
      localStorage['movies'] = JSON.stringify(this.movies);
    }
  }

  public deleteMovie(id?: string) {
    const movie = this.movies.find((x) => {
      return x.Id === id;
    });
    if (id) {
      this.apiService.DeleteMovie(id);
      if (movie) {
        const index = this.movies.findIndex((movie) => {
          return movie.Id === id;
        });
        this.movies.splice(index, 1);
      }
    }
  }
}
