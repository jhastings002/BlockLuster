import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AddMovieRequest } from '../contacts/add-movie-request';
import { Movie } from '../contacts/movie';
import { RentMoviesRequest } from '../contacts/rent-movies-request';
import { SigninRequest } from '../contacts/signin-request';
import { SignupRequest } from '../contacts/signup-request';
import { UpdatePasswordRequest } from '../contacts/update-password.request';
import { UpdateProfileRequest } from '../contacts/update-profile.request';
import { User } from '../contacts/user';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  constructor(private http: HttpClient) {}

  public async GetCatalog(): Promise<any> {
    const options = this.HttpOptions();
    const result = await this.http
      .get<string>(`${environment.apiUrl}/GetCatalog`)
      .toPromise();
    return result;
  }

  public async SignIn(signinRequest: SigninRequest): Promise<any> {
    const options = this.HttpOptions();
    const result = await this.http.post<any>(
      `${environment.apiUrl}/SignIn`,
      JSON.stringify(signinRequest)
    );
    try {
      const returnResult = await result.toPromise();
      if (returnResult.Success) {
        localStorage['token'] = returnResult.Token;
        localStorage['user'] = JSON.stringify(returnResult.User);
        localStorage['admin'] = returnResult.isAdmin;
        localStorage['rentedmovies'] = JSON.stringify(
          returnResult.RentedMovies
        );
        return true;
      }
      return false;
    } catch {
      return false;
    }
  }

  public async SignUp(signupRequest: SignupRequest): Promise<boolean> {
    const options = this.HttpOptions();
    const result = await this.http.post<any>(
      `${environment.apiUrl}/SignUp`,
      JSON.stringify(signupRequest)
    );
    try {
      const returnResult = await result.toPromise();
      if (returnResult.Success) {
        localStorage['token'] = returnResult.Token;
        localStorage['user'] = JSON.stringify(returnResult.User);
        localStorage['admin'] = returnResult.isAdmin;
        localStorage['rentedmovies'] = JSON.stringify(
          returnResult.RentedMovies
        );
        return true;
      }
      return false;
    } catch {
      return false;
    }
  }

  public async UpdateProfile(
    updateRequest: UpdateProfileRequest
  ): Promise<boolean> {
    const options = this.HttpOptions();
    const result = await this.http.post<any>(
      `${environment.apiUrl}/UpdateProfile`,
      JSON.stringify(updateRequest)
    );
    try {
      const returnResult = await result.toPromise();
      if (returnResult) {
        return true;
      }
      return false;
    } catch {
      return false;
    }
  }

  public async UpdatePassword(
    updateRequest: UpdatePasswordRequest
  ): Promise<boolean> {
    const options = this.HttpOptions();
    const result = await this.http.post<any>(
      `${environment.apiUrl}/UpdatePassword`,
      JSON.stringify(updateRequest)
    );
    try {
      const returnResult = await result.toPromise();
      if (returnResult) {
        return true;
      }
      return false;
    } catch {
      return false;
    }
  }

  public async SignOut(): Promise<boolean> {
    localStorage.clear();
    return true;
  }

  public IsLoggedIn(): boolean {
    var token = localStorage['token'];
    if (!token) {
      return false;
    } else {
      return true;
    }
  }

  public IsAdmin(): boolean {
    var admin = localStorage['admin'];
    if (admin && admin === 'true') {
      return true;
    } else {
      return false;
    }
  }

  public RentedMovies(): Movie[] {
    return JSON.parse(localStorage['rentedmovies']);
  }

  public Cart(): Movie[] {
    return JSON.parse(localStorage['cart']);
  }

  public User(): User {
    return JSON.parse(localStorage['user']);
  }
  public SetUser(user: User) {
    localStorage['user'] = JSON.stringify(user);
  }

  public async AddMovie(addMovieRequest: AddMovieRequest): Promise<boolean> {
    const options = this.HttpOptions();
    const result = await this.http.post<any>(
      `${environment.apiUrl}/AddMovie`,
      JSON.stringify(addMovieRequest)
    );
    try {
      const returnResult = await result.toPromise();
      if (returnResult) {
        return true;
      }
      return false;
    } catch {
      return false;
    }
  }

  public async DeleteMovie(id: string): Promise<boolean> {
    const options = this.HttpOptions();
    const result = await this.http.get<any>(
      `${environment.apiUrl}/RemoveMovie?movieId=${id}`
    );
    try {
      const returnResult = await result.toPromise();
      if (returnResult) {
        return true;
      }
      return false;
    } catch {
      return false;
    }
  }

  public async CheckOutMovies(movies: Movie[]): Promise<Movie[]> {
    const options = this.HttpOptions();
    let movieIds: string[] = [];
    for (let movie of movies) {
      if (movie.Id) {
        movieIds.push(movie.Id);
      }
    }
    let userId = this.User().Id;
    let rentMoviesRequest: RentMoviesRequest = {
      UserId: userId,
      MovieIds: movieIds,
    };
    const result = await this.http.post<any>(
      `${environment.apiUrl}/RentMovie`,
      JSON.stringify(rentMoviesRequest)
    );
    try {
      const returnResult = await result.toPromise();

      localStorage['rentedmovies'] = JSON.stringify(returnResult);
      return returnResult;
    } catch {
      return [];
    }
  }

  public async ReturnMovie(movieId: string): Promise<Movie[]> {
    const options = this.HttpOptions();
    let userId = this.User().Id;
    const result = await this.http.post<any>(
      `${environment.apiUrl}/ReturnMovie`,
      JSON.stringify({ UserId: userId, MovieId: movieId })
    );
    try {
      const returnResult = await result.toPromise();

      localStorage['rentedmovies'] = JSON.stringify(returnResult);
      return returnResult;
    } catch {
      return [];
    }
  }

  private HttpOptions(): { headers: HttpHeaders } {
    return {
      headers: new HttpHeaders({
        Accept: 'application/json',
        Authorization: `Bearer ${'token'}`,
      }),
    };
  }
}
