import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-change-passowrd',
  templateUrl: './change-passowrd.component.html',
  styleUrls: ['./change-passowrd.component.css']
})
export class ChangePassowrdComponent implements OnInit {

  isAllowed: boolean;
  changePasswordForm: FormGroup;
  
  constructor(private fb: FormBuilder, private route: ActivatedRoute, private userService: UserService) {

    this.changePasswordForm = this.fb.group({
      password : ['', Validators.required],
      confirmpassword : ['', Validators.required]
    });

    this.route.params.subscribe(params => {
      this.userService.canChangePasswordByEmail(params.id, params.playerHash).subscribe(result => {
        this.isAllowed = result;
      });
    });
   }
  ngOnInit(): void {

  }

  get Password() {
    return this.changePasswordForm.get('password');
  }

  get ConfirmPassword() {
    return this.changePasswordForm.get('confirmpassword');
  }

  change() {
    if (this.Password.value === this.ConfirmPassword.value) {
      // Send Password
    } else {
      //
    }
  }
}
