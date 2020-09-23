import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IdentityService } from '../../services/identity.service';

@Component({
  selector: 'app-password-forgotten',
  templateUrl: './password-forgotten.component.html',
  styleUrls: ['./password-forgotten.component.css']
})
export class PasswordForgottenComponent implements OnInit {

  fpForm: FormGroup;
  message: string;
  isSuccess: boolean;
  constructor(private fb: FormBuilder, private identityServices: IdentityService, private router: Router) {
     this.isSuccess = false;
     this.fpForm = this.fb.group({
      email : ['', Validators.required],
    });
    }

  ngOnInit(): void {
  }

  get Email() {
    return this.fpForm.get('email');
  }

  remind() {
    this.identityServices.changePassword(this.Email.value).subscribe(res => {
      if (res === true) {
        this.message = 'Na podany adres został wysłany link';
        this.isSuccess = true;
      } else {
        this.isSuccess = false;
      }

      this.router.navigate(['login']);

    });
  }
}
