import { Injectable } from '@angular/core';
import { Session, User } from '../Models/Session/Session';

@Injectable({
  providedIn: 'root'
})
export class SessionService {

  private session: Session;

  constructor() {
    this.session = new Session();
  }

  SetNewSeesion(user: User, token: string, gameToken: string) {
    this.session.user = user;
    this.session.token = token;
    this.session.gameToken = gameToken;
  }

  GetCurrentUser(): User {
    return this.session.user;
  }

  GetToken(): string {
    return this.session.token;
  }
}
