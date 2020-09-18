import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginResponseModel } from '../Models/Identity/loginResponseModel';
import { environment } from './../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private controllerUrl = environment.apiUrl + 'Identity/';

  constructor(private http: HttpClient) { }

  login(data): Observable<LoginResponseModel> {
    return this.http.post<LoginResponseModel>(this.controllerUrl + 'Login', data);
  }

  saveToken(token) {
    localStorage.setItem('token', token);
  }

  getToken() {
    return localStorage.getItem('token');
  }

  isAuthenticated(): boolean {

     if (this.getToken()) {
       return true;
     }

     return false;
  }
}
