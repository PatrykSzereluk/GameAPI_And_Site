import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserRanking } from '../Models/Identity/UserRanking';

@Injectable({
  providedIn: 'root'
})
export class RankingService {

  constructor(private http: HttpClient) { }

  getRanking(data): Observable<Array<UserRanking>> {
    return this.http.post<Array<UserRanking>>('https://localhost:44343/Ranking/GetUserRanking', data);
  }
}
