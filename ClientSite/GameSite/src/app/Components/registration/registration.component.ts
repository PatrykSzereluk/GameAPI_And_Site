import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserRegisterRequestModel } from '../../Models/Identity/UserRegister';
import { Router } from '@angular/router';
import { IdentityService } from '../../services/identity.service';
import { UserService } from '../../services/user.service';
import { SessionService } from '../../services/session.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  hideForm = false;
  registerForm: FormGroup;
  registerResponseError = false;
  passwordError = false;
  loginError = false;
  nickNameError = false;
  emailError = false;

  constructor(private fb: FormBuilder,
              private authService: IdentityService,
              private userService: UserService) {
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
    this.loginError = false;
    this.nickNameError = false;
    this.emailError = false;

    if (!this.checkPassword()) {
      this.passwordError = true;
      return;
    }

    const registerData = new UserRegisterRequestModel();

    registerData.NickName = this.NickName.value;
    registerData.Login = this.Login.value;
    registerData.Password = this.Password.value;
    registerData.Email = this.Email.value;

    this.userService.checkLogin(registerData.Login).subscribe(loginRes => {
      if (loginRes === true) {
        this.userService.checkNickName(registerData.NickName).subscribe(nickNameRes => {
          if (nickNameRes === true) {
            this.userService.checkEmail(registerData.Email).subscribe(emailRes => {
              if (emailRes === true) {
                this.authService.register(registerData).subscribe( res => {
                  if (!res.isSuccess) {
                    if (res.statusCode === 456 || res.playerId === -1) {
                      this.registerResponseError = true;
                    }
                  } else {
                    this.hideForm = true; // OK
                  }
                });
              } else {
                this.emailError = true;
              }
            });
          } else {
            this.nickNameError = true;
          }
        });
      } else {
        this.loginError = true;
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
