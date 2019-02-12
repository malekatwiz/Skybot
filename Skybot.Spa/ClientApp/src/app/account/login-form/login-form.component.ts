import { Component, OnInit } from '@angular/core';

import { LoginCredentials } from '../../models/login-credentials';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent implements OnInit {
  credentials: LoginCredentials = {phoneNumber: '', accessCode: null};

  constructor(private accountService: AccountService) { }

  ngOnInit() {
  }

  login({ value, valid }: { value: LoginCredentials, valid: boolean }) {
    if (valid) {
      this.accountService.login(value.phoneNumber, value.accessCode);
    }
  }
}
