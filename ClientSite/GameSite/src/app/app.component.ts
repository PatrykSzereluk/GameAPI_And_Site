import { Component } from '@angular/core';
import { IdentityService } from './services/identity.service';
import {ApplicationRef } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  title = 'GameSite';
  isAuthenticate = false;

  constructor(private identityService: IdentityService, private ref: ApplicationRef) {
    this.isAuthenticate = identityService.isAuthenticated();
  }

  Refresh() {
    this.ref.tick();
 }

 isAuth(): boolean {
   return this.identityService.isAuthenticated();
 }

}
