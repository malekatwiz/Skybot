import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SkybotFormComponent } from './skybot-form/skybot-form.component';

const routes: Routes = [
  { path: 'query', component: SkybotFormComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SkybotRoutingModule { }
