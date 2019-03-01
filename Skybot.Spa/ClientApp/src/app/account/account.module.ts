import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { VerificationFormComponent } from './verification-form/verification-form.component';
import { AccountService } from './account.service';
import { AuthService } from '../auth/auth.service';
import { SmsService } from '../sms/sms.service';
import { CreateFormComponent } from './create-form/create-form.component';
import { AccessCodeComponent } from './access-code/access-code.component';

const accountRoutes: Routes = [
  {path: 'account/code', component: AccessCodeComponent},
  { path: 'account/verify', component: VerificationFormComponent },
  {path: 'account/create', component: CreateFormComponent}
];

@NgModule({
  declarations: [VerificationFormComponent, CreateFormComponent, AccessCodeComponent],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild(accountRoutes)
  ],
  providers: [AccountService, AuthService, SmsService],
  exports: [
    RouterModule]
})
export class AccountModule { }
