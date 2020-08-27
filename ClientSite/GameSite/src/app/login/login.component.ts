import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;

  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.loginForm = this.fb.group({
      login : ['', Validators.required],
      password : ['', Validators.required]
    });
  }

  ngOnInit(): void {
  }

  login() {
    this.authService.login(this.loginForm.value).subscribe(data => {
      console.log(data);
    });
  }

  get Login() {
    return this.loginForm.get('login');
  }

  get Password() {
    return this.loginForm.get('password');
  }

}
