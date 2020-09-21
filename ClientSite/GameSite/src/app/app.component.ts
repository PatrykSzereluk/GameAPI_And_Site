import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  title = 'GameSite';
  isAuthenticate = false;

  constructor(private auth: AuthService) {
    this.isAuthenticate = auth.isAuthenticated();
  }



}
