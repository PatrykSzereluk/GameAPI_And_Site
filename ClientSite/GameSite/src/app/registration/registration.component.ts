import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  registerForm: FormGroup;

  constructor(private fb: FormBuilder) {
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
