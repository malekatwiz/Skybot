import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Rx';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http: Http) { }

  login(phoneNumber, accessCode) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');

    //return this.http.post('',
    //  JSON.stringify({ phoneNumber, accessCode }),
    //  { headers })
    //  .map(res => res.json())
    //  .map(res => {
    //    localStorage.setItem('auth_token', res.auth_token);
    //    return true;
    //  });
  }
}
