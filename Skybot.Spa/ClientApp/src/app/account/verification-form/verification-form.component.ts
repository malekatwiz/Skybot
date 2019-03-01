import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AccountService } from '../account.service';
import { VerificationModel } from './verification-model';

@Component({
  selector: 'app-verification-form',
  templateUrl: './verification-form.component.html',
  styleUrls: ['./verification-form.component.css']
})
export class VerificationFormComponent implements OnInit {

  model: VerificationModel = {phoneNumber: '', accessCode: null};
  constructor(private readonly accountService: AccountService,
    private readonly router: Router) { }

  ngOnInit() {
  }

  async verify({ value, valid }: { value: VerificationModel, valid: boolean }) {
    if (valid) {
      const isValid = await this.accountService.verifyAccessCode(value.phoneNumber, value.accessCode).then(
        r => r.success);

      if (isValid) {
        this.router.navigate(['/']);
      }
    }
  }
}
