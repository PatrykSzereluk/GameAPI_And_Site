import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';
import {ApplicationRef } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  title = 'GameSite';
  isAuthenticate = false;

  constructor(private auth: AuthService, private ref: ApplicationRef) {
    this.isAuthenticate = auth.isAuthenticated();
  }

  Refresh() {
    this.ref.tick();
 }

 isAuth(): boolean {
   return this.auth.isAuthenticated();
 }

}
