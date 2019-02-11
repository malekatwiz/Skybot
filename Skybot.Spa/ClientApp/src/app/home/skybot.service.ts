import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SkybotService {

  constructor(private httpClient: HttpClient) { }

  processQuery(query: string) {

  }
}
