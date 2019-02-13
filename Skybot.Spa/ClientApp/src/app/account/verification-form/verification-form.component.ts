import { Component, OnInit } from '@angular/core';

import { VerificationModel } from './verification-model';

@Component({
  selector: 'app-verification-form',
  templateUrl: './verification-form.component.html',
  styleUrls: ['./verification-form.component.css']
})
export class VerificationFormComponent implements OnInit {

  model: VerificationModel = {phoneNumber: '', accessCode: null};
  constructor() { }

  ngOnInit() {
  }

  verify({ value, valid }: { value: VerificationModel, valid: boolean }) {

  }
}
