import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RankingService {

  constructor(private http: HttpClient) { }

  getRanking(data): Observable<any> {
    return this.http.post('https://localhost:44343/Ranking/GetUserRanking', data);
  }
}
