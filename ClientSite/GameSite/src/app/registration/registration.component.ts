import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { UserRegisterRequestModel } from '../Models/Identity/UserRegister';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  registerForm: FormGroup;
  registerResponseError = false;
  passwordError = false;

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.registerForm = this.fb.group({
      login : ['', Validators.required],
      nickName: ['', Validators.required],
      email: ['', Validators.required],
      password : ['', Validators.required],
      repeatPassword : ['', Validators.required]
    });
  }

  ngOnInit(): void {
  }

  register() {
    this.passwordError = false;

    if (!this.checkPassword()) {
      this.passwordError = true;
      return;
    }

    const registerData = new UserRegisterRequestModel();

    registerData.NickName = this.NickName.value;
    registerData.Login = this.Login.value;
    registerData.Password = this.Password.value;
    registerData.Email = this.Email.value;

    this.authService.register(registerData).subscribe( res => {

      if (!res.isSuccess) {
        if (res.statusCode === 456 || res.playerId === -1) {
          this.registerResponseError = true;
        }
      } else {
        this.router.navigate(['home']);
      }
    });
  }

  checkPassword(): boolean {
    return this.Password.value === this.RepeatPassword.value;
  }

  get Login() {
    return this.registerForm.get('login');
  }

  get NickName() {
    return this.registerForm.get('nickName');
  }

  get Email() {
    return this.registerForm.get('email');
  }

  get Password() {
    return this.registerForm.get('password');
  }

  get RepeatPassword() {
    return this.registerForm.get('repeatPassword');
  }

}
