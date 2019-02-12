import { Component, OnInit } from '@angular/core';

import { SkybotQuery } from '../../models/skybot-query';

@Component({
  selector: 'app-skybot-form',
  templateUrl: './skybot-form.component.html',
  styleUrls: ['./skybot-form.component.css']
})
export class SkybotFormComponent implements OnInit {

  model: SkybotQuery = {query: ''};
  constructor() { }

  ngOnInit() {
  }

  sendQuery({ value, valid }: { value: SkybotQuery, valid: boolean }) {

  }
}
