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

  SetNewSeesion(user: User, token: string) {
    this.session.user = user;
    this.session.token = token;
  }

  GetCurrentUser(): User {
    return this.session.user;
  }

}
