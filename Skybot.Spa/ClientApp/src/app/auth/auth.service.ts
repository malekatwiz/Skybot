import { Injectable } from '@angular/core';
import { HttpHeaders, HttpParams, HttpClient } from '@angular/common/http';

import { AccessTokenModel } from './access-token-model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient) { }

  public async getAccessToken(): Promise<AccessTokenModel> {
    var headers = new HttpHeaders();
    headers.append('Content-Type', 'application/x-www-form-urlencoded');

    var bodyContent = new HttpParams()
    .append('client_id', 'client_id')
    .append('client_secret', 'client_secret')
    .append('grant_type', 'client_credentials');

    const res = await this.http.post<AccessTokenModel>('https://auth.skybot.io/connect/token',
      bodyContent,
      { headers: headers, withCredentials: true, observe: 'body', responseType: 'json' }).toPromise();
    return res;
  }
}
