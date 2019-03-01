import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';

import { AuthService } from '../auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class SmsService {
  private token: string;

  constructor(private readonly authService: AuthService,
    private readonly http: HttpClient) { }

  async send(phoneNumber: string, message: string): Promise<HttpResponse<Object>> {
    const headers = await this.getHeadersWithToken();

    const response = await this.http.post('https://texto.skybot.io/api/text/send',
      JSON.stringify({
        toNumber: phoneNumber,
        message: message
      }),
      { headers: headers, observe: 'response' }).toPromise();

    return response;
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
