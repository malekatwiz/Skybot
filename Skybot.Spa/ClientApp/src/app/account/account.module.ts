import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { LoginFormComponent } from './login-form/login-form.component';
import { AccountService } from './account.service';

const accountRoutes: Routes = [
  { path: 'account/login', component: LoginFormComponent }
];

@NgModule({
  declarations: [LoginFormComponent],
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
