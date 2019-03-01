import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AccountService } from '../account.service';

@Component({
  selector: 'app-access-code',
  templateUrl: './access-code.component.html',
  styleUrls: ['./access-code.component.css']
})
export class AccessCodeComponent implements OnInit {
  constructor(private readonly accountService: AccountService,
    private readonly router: Router) {
  }

  ngOnInit() {
  }

  send(phoneNumber: string) {
    this.accountService.hasAccount(phoneNumber).then(p => {
      p.subscribe(async (res) => {
        if (res.success) {
          const result = await this.accountService.sendAccessCode(phoneNumber).then(r => r);
          if (result) {
            this.router.navigate(['account/verify']);
          }
        } else {
          this.router.navigate(['account/create']);
        }
      });
    });
  }
}
