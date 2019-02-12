import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { SkybotRoutingModule } from './skybot-routing.module';
import { SkybotFormComponent } from './skybot-form/skybot-form.component';

@NgModule({
  declarations: [SkybotFormComponent],
  imports: [
    CommonModule,
    FormsModule,
    SkybotRoutingModule
  ]
})
export class SkybotModule { }
