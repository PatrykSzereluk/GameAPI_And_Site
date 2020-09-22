import { Component, OnInit } from '@angular/core';
import {ApplicationRef } from '@angular/core';
import { SessionService } from '../services/session.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private ref: ApplicationRef, private sessionService: SessionService) {
    this.Refresh();
  }

  ngOnInit(): void {
  }

  Refresh() {
    this.ref.tick();
 }

 GetUserNickName() {
   return this.sessionService.GetCurrentUser().nickName;
 }


}
