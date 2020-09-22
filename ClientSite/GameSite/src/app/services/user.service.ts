import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EmailConfirmationResponseModel } from '../Models/Email/EmailConfirmationResponseModel';
import { environment } from './../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private controllerUrl = environment.apiUrl + 'User/';

  constructor(private http: HttpClient) { }

  getUserDetails(id): Observable<any> {
    return this.http.post(this.controllerUrl + 'GetUserDetails', id);
  }

  confirmUserEmail(id, playerHash): Observable<EmailConfirmationResponseModel> {
    return this.http.post<EmailConfirmationResponseModel>(this.controllerUrl + 'ConfirmUserEmail',
     {PlayerId: Number.parseInt(id, 0), PlayerHash: playerHash });
  }

  canChangePasswordByEmail(id, playerHash): Observable<boolean> {
    return this.http.post<boolean>(this.controllerUrl + 'CanChangePasswordByEmail',
     {PlayerId: Number.parseInt(id, 0), PlayerHash: playerHash });
  }

  changePasswordByEmailSecondStep(id, playerHash, newPassword): Observable<boolean> {
    return this.http.post<boolean>(this.controllerUrl + 'ChangePasswordByEmailSecondStep',
     {PlayerId: Number.parseInt(id, 0), PlayerHash: playerHash, Password: newPassword });
  }

  checkLogin(loginParam: string): Observable<boolean> {
    return this.http.post<boolean>(this.controllerUrl + 'CheckLoginExists', {login: loginParam});
  }

  checkNickName(nickNameParam: string): Observable<boolean> {
    return this.http.post<boolean>(this.controllerUrl + 'CheckNickNameExists', {nickName: nickNameParam});
  }

  CheckEmail(emailParam: string): Observable<boolean> {
    return this.http.post<boolean>(this.controllerUrl + 'CheckEmailExists', {email: emailParam});
  }

  
}

