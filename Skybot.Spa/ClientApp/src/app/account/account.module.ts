import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { VerificationFormComponent } from './verification-form/verification-form.component';
import { AccessCodeFormComponent } from './access-code-form/access-code-form.component';
import { AccountService } from './account.service';

const accountRoutes: Routes = [
  {path: 'account/code', component: AccessCodeFormComponent},
  { path: 'account/verify', component: VerificationFormComponent }
];

@NgModule({
  declarations: [VerificationFormComponent, AccessCodeFormComponent],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild(accountRoutes)
  ],
  providers: [AccountService],
  exports: [
    RouterModule]
})
export class AccountModule { }
