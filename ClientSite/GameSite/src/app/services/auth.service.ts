import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginResponseModel } from '../Models/Identity/loginResponseModel';
import { environment } from './../../environments/environment';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private controllerUrl = environment.apiUrl + 'Identity/';

  constructor(private http: HttpClient, private router: Router) { }

  login(data): Observable<LoginResponseModel> {
    return this.http.post<LoginResponseModel>(this.controllerUrl + 'Login', data);
  }

  saveToken(token) {
    localStorage.setItem('token', token);
  }

  getToken() {
    return localStorage.getItem('token');
  }

  logout() {
   localStorage.removeItem('token');
   this.router.navigate(['login']);
  }

  isAuthenticated(): boolean {

    const token = this.getToken();

    if (this.getToken() && token !== 'null') {
       return true;
    }

    return false;
  }
}
