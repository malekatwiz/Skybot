import { Component, OnInit } from '@angular/core';

import { AccessCodeModel } from './access-code-model';

@Component({
  selector: 'app-access-code-form',
  templateUrl: './access-code-form.component.html',
  styleUrls: ['./access-code-form.component.css']
})
export class AccessCodeFormComponent implements OnInit {
  model: AccessCodeModel = {phoneNumber: ''};
  constructor() { }

  ngOnInit() {
  }

  send({ value, valid }: { value: AccessCodeModel, valid: boolean }) {

  }}
