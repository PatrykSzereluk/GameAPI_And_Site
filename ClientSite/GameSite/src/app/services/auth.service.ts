import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginResponseModel } from '../Models/Identity/loginResponseModel';
import { environment } from './../../environments/environment';
import { ITS_JUST_ANGULAR } from '@angular/core/src/r3_symbols';
import { isNull } from '@angular/compiler/src/output/output_ast';

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

    let token = this.getToken();



     if (this.getToken() && token !== 'null') {
       return true;
     }

     return false;
  }
}
