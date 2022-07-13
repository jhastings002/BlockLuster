import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { UpdatePasswordRequest } from '../contacts/update-password.request';
import { UpdateProfileRequest } from '../contacts/update-profile.request';
import { User } from '../contacts/user';
import { ApiService } from '../services/api-service.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  user: User = {} as User;
  updateForm: FormGroup = this.formBuilder.group({
    firstNameControl: new FormControl('', [Validators.required]),
    lastNameControl: new FormControl('', [Validators.required]),
    emailControl: new FormControl({ value: '', disabled: true }),
    oldPasswordControl: new FormControl('', [Validators.required]),
    passwordControl: new FormControl('', [Validators.required]),
  });

  updateError: boolean = false;

  constructor(
    private apiService: ApiService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.user = this.apiService.User();

    this.updateForm.get('firstNameControl')?.setValue(this.user.FirstName);
    this.updateForm.get('lastNameControl')?.setValue(this.user.LastName);
    this.updateForm.get('emailControl')?.setValue(this.user.Email);
    this.updateForm.get('emailControl')?.disable;
  }

  async updateProfile() {
    const updateRequest: UpdateProfileRequest = {
      userId: this.user.Id,
      firstName: this.updateForm?.get('firstNameControl')?.value?.toString(),
      lastName: this.updateForm?.get('lastNameControl')?.value?.toString(),
    };

    const result = await this.apiService.UpdateProfile(updateRequest);

    if (result) {
      this.user.FirstName = updateRequest.firstName;
      this.user.LastName = updateRequest.lastName;
      this.apiService.SetUser(this.user);
      location.href = '/profile';
    } else {
      this.updateError = true;
    }
  }

  async updatePassword() {
    const updateRequest: UpdatePasswordRequest = {
      userId: this.user.Id,
      oldPassword: this.updateForm
        ?.get('oldPasswordControl')
        ?.value?.toString(),
      newPassword: this.updateForm?.get('passwordControl')?.value?.toString(),
    };

    const result = await this.apiService.UpdatePassword(updateRequest);
    if (result) {
      location.href = '/profile';
    } else {
      this.updateError = true;
    }
  }
}
