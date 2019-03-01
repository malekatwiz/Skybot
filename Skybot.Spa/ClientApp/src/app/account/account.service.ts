import { Injectable, OnInit } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';

import { AuthService } from '../auth/auth.service';
import { SmsService } from '../sms/sms.service';
import { CreateAccountModel } from './create-form/create-account-model';
import { AccountResponseModel } from './account-response-model';

@Injectable()
export class AccountService implements OnInit {
  private token: string;

  constructor(private readonly http: HttpClient,
    private readonly authService: AuthService,
    private readonly smsService: SmsService) { }

  ngOnInit() {
  }

  login(phoneNumber, accessCode) {
    let headers = new HttpHeaders();
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

  async hasAccount(phoneNumber: string): Promise<Observable<AccountResponseModel>> {
    var headers = await this.getHeadersWithToken();

    return this.http.get<AccountResponseModel>('https://accounts.skybot.io/api/accounts/check/' + phoneNumber,
      { headers: headers, observe: 'body' });
  }

  async create(model: CreateAccountModel) {
    var headers = await this.getHeadersWithToken();

    return this.http.put('https://accounts.skybot.io/api/accounts/create',
        JSON.stringify(model),
        { headers: headers, observe: 'response' });
  }

  async sendAccessCode(phoneNumber: string) {
    const accessCode = await this.requestAccessCode(phoneNumber)
      .then(p => p);

    if (accessCode.success) {
      var code = JSON.parse(accessCode.object) as number;
      const response = await this.smsService.send(phoneNumber, 'Your access code is: ' + code).then(r => r);
      if (response.status === 200) {
        return true;
      }
    }
    return false;
  }

  async verifyAccessCode(phoneNumber: string, accessCode: number): Promise<AccountResponseModel> {
    var headers = await this.getHeadersWithToken();

    const response = await this.http.post<AccountResponseModel>('https://accounts.skybot.io/api/accounts/validateaccesscode',
      JSON.stringify({
        phoneNumber: phoneNumber,
        code: accessCode
      }),
      { headers: headers, observe: 'body' }).toPromise();

    return response;
  }

  private async requestAccessCode(phoneNumber: string): Promise<AccountResponseModel> {
    var headers = await this.getHeadersWithToken();

    const model = {
      phoneNumber: phoneNumber
    };
    const code = await this.http.post<AccountResponseModel>('https://accounts.skybot.io/api/accounts/generateaccesscode',
      JSON.stringify(model),
      { headers: headers, observe: 'body' }).toPromise();

    return code;
  }

  private async requestAccessToken() {
    if (this.token == null || this.token === '') {
      await this.authService.getAccessToken()
        .then(t => this.token = t.access_token);
    }
  }

  private async getHeadersWithToken() {
    await this.requestAccessToken();

    return new HttpHeaders()
      .append('Content-Type', 'application/json')
      .append('Authorization', 'Bearer ' + this.token);
  }
}
