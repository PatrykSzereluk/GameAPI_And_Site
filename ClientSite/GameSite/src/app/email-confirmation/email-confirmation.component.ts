import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../services/user.service';
import { EmailConfirmationResponseModel } from '../Models/Email/EmailConfirmationResponseModel';

@Component({
  selector: 'app-email-confirmation',
  templateUrl: './email-confirmation.component.html',
  styleUrls: ['./email-confirmation.component.css']
})
export class EmailConfirmationComponent implements OnInit {

  emailConfirmModel: EmailConfirmationResponseModel;

  constructor(private route: ActivatedRoute, private userService: UserService) {

    this.route.params.subscribe(params => {
      this.userService.confirmUserEmail(params.id, params.playerHash).subscribe(res1 => {
        this.emailConfirmModel = res1;
      });
    });
  }

  ngOnInit(): void {
  }

}
