import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { SignupRequest } from '../contacts/signup-request';
import { ApiService } from '../services/api-service.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
})
export class SignupComponent implements OnInit {
  signupForm: FormGroup = this.formBuilder.group({
    firstNameControl: new FormControl('', [Validators.required]),
    lastNameControl: new FormControl('', [Validators.required]),
    emailControl: new FormControl('', [Validators.required, Validators.email]),
    passwordControl: new FormControl('', [Validators.required]),
  });

  signupError: boolean = false;

  constructor(
    private apiService: ApiService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {}

  ngOnInit(): void {}

  async Signup() {
    const signupRequest: SignupRequest = {
      firstName: this.signupForm?.get('firstNameControl')?.value?.toString(),
      lastName: this.signupForm?.get('lastNameControl')?.value?.toString(),
      email: this.signupForm?.get('emailControl')?.value?.toString(),
      password: this.signupForm?.get('passwordControl')?.value?.toString(),
    };

    var result = await this.apiService.SignUp(signupRequest);
    if (result) {
      location.href = '/';
    } else {
      this.signupError = true;
    }
  }
}
