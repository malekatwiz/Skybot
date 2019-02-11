import { Component } from '@angular/core';
import { SkybotQuery } from '../models/SkybotQuery';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  skybotQuery: SkybotQuery = {
    query: null
  };
}
