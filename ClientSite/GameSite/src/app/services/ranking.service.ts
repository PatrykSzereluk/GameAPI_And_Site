import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserRanking } from '../Models/Identity/UserRanking';
import { environment } from './../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RankingService {

  private controllerUrl = environment.apiUrl + 'Ranking/';

  constructor(private http: HttpClient) { }

  getRanking(data): Observable<Array<UserRanking>> {
    return this.http.post<Array<UserRanking>>(this.controllerUrl + 'GetUserRanking', data);
  }
}
