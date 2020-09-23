import { Component, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginResponseModel } from '../../Models/Identity/loginResponseModel';
import { User } from '../../Models/Session/Session';
import { IdentityService } from '../../services/identity.service';
import { SessionService } from '../../services/session.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  loginError = false;


  constructor(
     private fb: FormBuilder,
     private identityService: IdentityService,
     private router: Router,
     private sessionService: SessionService) {
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

        // remove from cookies
        this.identityService.saveToken(data.token);

        const user = new User();
        user.playerId = data.playerId;
        user.nickName = data.playerNickName;
        this.sessionService.SetNewSeesion(user, data.token, data.gameToken);
        // remove from cookies

        this.sessionService.SetNewSeesion(this.createNewUser(data), data.token, data.gameToken);

        this.router.navigate(['home']);
      } else {
        this.loginError = true;
      }
    });
  }

  private createNewUser(data: LoginResponseModel): User {
      const user = new User();
      user.playerId = data.playerId;
      user.nickName = data.playerNickName;
      return user;
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
