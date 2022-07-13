import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { SigninRequest } from '../contacts/signin-request';
import { ApiService } from '../services/api-service.service';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.scss'],
})
export class SigninComponent implements OnInit {
  signinForm: FormGroup = this.formBuilder.group({
    emailControl: new FormControl('', [Validators.required, Validators.email]),
    passwordControl: new FormControl('', [Validators.required]),
  });

  signinError: boolean = false;

  constructor(
    private apiService: ApiService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {}

  ngOnInit(): void {}

  async Signin() {
    const signinRequest: SigninRequest = {
      email: this.signinForm?.get('emailControl')?.value?.toString(),
      password: this.signinForm?.get('passwordControl')?.value?.toString(),
    };

    var result = await this.apiService.SignIn(signinRequest);
    if (result) {
      location.href = '/';
    } else {
      this.signinError = true;
    }
  }
}
