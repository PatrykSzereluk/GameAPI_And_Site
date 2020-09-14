import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EmailConfirmationResponseModel } from '../Models/Email/EmailConfirmationResponseModel';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getUserDetails(id): Observable<any> {
    return this.http.post('https://localhost:44343/User/GetUserDetails', id);
  }

  confirmUserEmail(id, playerHash): Observable<EmailConfirmationResponseModel> {
    return this.http.post<EmailConfirmationResponseModel>('https://localhost:44343/User/ConfirmUserEmail',
     {PlayerId: Number.parseInt(id, 0), PlayerHash: playerHash });
  }

  canChangePasswordByEmail(id, playerHash): Observable<boolean> {
    return this.http.post<boolean>('https://localhost:44343/User/CanChangePasswordByEmail',
     {PlayerId: Number.parseInt(id, 0), PlayerHash: playerHash });
  }

}

