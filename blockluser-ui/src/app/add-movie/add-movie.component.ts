import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { AddMovieRequest } from '../contacts/add-movie-request';
import { RatingsMapper, Ratings } from '../contacts/enums/ratings.enum';
import { ApiService } from '../services/api-service.service';

@Component({
  selector: 'app-add-movie',
  templateUrl: './add-movie.component.html',
  styleUrls: ['./add-movie.component.scss'],
})
export class AddMovieComponent implements OnInit {
  addMovieForm: FormGroup = this.formBuilder.group({
    titleControl: new FormControl('', [Validators.required]),
    descriptionControl: new FormControl('', [Validators.required]),
    ratingControl: new FormControl('', [Validators.required, Validators.email]),
    dailyRateControl: new FormControl('', [Validators.required]),
  });

  addMovieError: boolean = false;
  rating = Ratings;
  ratingsMapper = RatingsMapper;

  constructor(
    private apiService: ApiService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {}

  ngOnInit(): void {}

  async AddMovie() {
    const addMovieRequest: AddMovieRequest = {
      Title: this.addMovieForm?.get('titleControl')?.value?.toString(),
      Description: this.addMovieForm
        ?.get('descriptionControl')
        ?.value?.toString(),
      Rating: this.addMovieForm?.get('ratingControl')?.value?.toString(),
      DailyRate: this.addMovieForm?.get('dailyRateControl')?.value?.toString(),
    };

    var result = await this.apiService.AddMovie(addMovieRequest);
    if (result) {
      //this.router.navigate(['/']).then(() => window.location.reload());
      this.router
        .navigateByUrl('/', { skipLocationChange: true })
        .then(() => this.router.navigate(['/']));
    } else {
      this.addMovieError = true;
    }
  }
}
