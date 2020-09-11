import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NumberValueAccessor } from '@angular/forms';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-email-confirmation',
  templateUrl: './email-confirmation.component.html',
  styleUrls: ['./email-confirmation.component.css']
})
export class EmailConfirmationComponent implements OnInit {

  withouterror: boolean;

  constructor(private route: ActivatedRoute, private userService: UserService) {
    this.withouterror = true;
    this.route.params.subscribe(res => {
      this.userService.confirmUserEmail(res.id, res.playerHash).subscribe(res1 => {
        this.withouterror = res1;
      });
    });
  }

  ngOnInit(): void {
  }

}
