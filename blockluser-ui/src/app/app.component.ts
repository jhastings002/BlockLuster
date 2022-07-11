import { Component, OnInit } from '@angular/core';
import { ApiService } from './services/api-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'blockluser-ui';
  movies: any;
  cart: any[] = [];
  constructor (private apiService: ApiService){}
  async ngOnInit(): Promise<void> {
    this.movies = await this.apiService.GetCatalog()

    console.log(this.movies)
  }

  public addToCart(id: string){
    this.cart.push(this.movies.find((movie: { Id: string; }) => { return movie.Id === id}));
    const index = this.movies.findIndex((movie: { Id: string; }) => { return movie.Id === id});
    this.movies.splice(index, 1);
    console.log(this.cart);
  }
}
