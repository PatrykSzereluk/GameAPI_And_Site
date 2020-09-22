import { Component, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IdentityService } from '../services/identity.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  loginError = false;


  constructor(private fb: FormBuilder, private identityService: IdentityService, private router: Router) {
    this.loginForm = this.fb.group({
      login : ['', Validators.required],
      password : ['', Validators.required]
    });
  }

  ngOnInit(): void {
  }

  login() {
    this.identityService.login(this.loginForm.value).subscribe(data => {
      if (data.playerId !== -1) {
        this.loginError = false;
        this.identityService.saveToken(data.token);
        this.router.navigate(['home']);
      } else {
        this.loginError = true;
      }
    });
  }

  get Login() {
    return this.loginForm.get('login');
  }

  get Password() {
    return this.loginForm.get('password');
  }
  // myForm = new FormGroup({
  //   name: new FormControl('', [Validators.required, Validators.minLength(3)]),
  //   file: new FormControl('', [Validators.required]),
  //   fileSource: new FormControl('', [Validators.required])
  // });

  // get f() {
  //   return this.myForm.controls;
  // }

  // onFileChange(event) {
  //   if (event.target.files.length > 0) {
  //     const file = event.target.files[0];
  //     this.myForm.patchValue({
  //       fileSource: file
  //     });
  //   }
  // }

  // submit() {
  //   const formData = new FormData();
  //   formData.append('file', this.myForm.get('fileSource').value);

  //   this.http.post('https://localhost:44343/Friend/UploadImage', formData)
  //     .subscribe(res => {
  //       console.log(res);
  //       alert('Uploaded Successfully.');
  //     });
  // }
}
