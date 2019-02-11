import { Component } from '@angular/core';
import { SkybotQuery } from '../models/skybot-query';
import { SkybotService } from './skybot.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  submitted = false;
  model: SkybotQuery = {
    query: null
  };

  constructor(skybotService: SkybotService) {

  }

  sendQuery() {
    this.submitted = true;
  }
}
