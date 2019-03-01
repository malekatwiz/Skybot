import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { CreateAccountModel } from './create-account-model';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-create-form',
  templateUrl: './create-form.component.html',
  styleUrls: ['./create-form.component.css']
})
export class CreateFormComponent implements OnInit {
  model: CreateAccountModel = { phoneNumber: '', name: '' };

  constructor(private accountService: AccountService, private router: Router) { }

  ngOnInit() {
  }

  public async create({ value, valid }: { value: CreateAccountModel, valid: boolean }) {
    if (valid) {
      await this.accountService.create(value).then(p => p.subscribe((res) => {
        if (res.status === 201) {
          this.router.navigate(['account/verify']);
        }
      }));
    }
  }
}
