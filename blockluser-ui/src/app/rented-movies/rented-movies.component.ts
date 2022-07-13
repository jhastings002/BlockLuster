import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RatingsMapper } from '../contacts/enums/ratings.enum';
import { Movie } from '../contacts/movie';
import { ApiService } from '../services/api-service.service';

@Component({
  selector: 'app-rented-movies',
  templateUrl: './rented-movies.component.html',
  styleUrls: ['./rented-movies.component.scss'],
})
export class RentedMoviesComponent implements OnInit {
  rentedMovies: Movie[] = [];
  ratingsMapper = RatingsMapper;

  constructor(private apiService: ApiService, private router: Router) {}

  ngOnInit(): void {
    var token = localStorage['token'];

    if (!this.apiService.IsLoggedIn()) {
      this.router.navigateByUrl('/');
    } else {
      this.rentedMovies = this.apiService.RentedMovies();
    }
  }

  public async returnMovie(id: string) {
    const returningMovie = this.rentedMovies.find((movie) => {
      return movie.Id === id;
    });

    const result = await this.apiService.ReturnMovie(returningMovie?.Id ?? '');

    if (result) {
      const index = this.rentedMovies.findIndex((movie) => {
        return movie.Id === id;
      });
      this.rentedMovies.splice(index, 1);
    }
    if (this.rentedMovies.length === 0) {
      location.href = '/';
    }
  }
}
