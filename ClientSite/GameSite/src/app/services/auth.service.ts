import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

login(data): Observable<any> {
  return this.http.post('https://localhost:44343/Identity/Login', data);
}

}
