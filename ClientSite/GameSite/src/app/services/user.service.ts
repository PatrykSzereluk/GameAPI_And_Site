import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getUserDetails(id) : Observable<any> {
    return this.http.post('https://localhost:44343/Ranking/GetUserRanking', id);
  }

}

