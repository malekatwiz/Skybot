import { TestBed, inject } from '@angular/core/testing';

import { SkybotService } from './skybot.service';

describe('SkybotService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SkybotService]
    });
  });

  it('should be created', inject([SkybotService], (service: SkybotService) => {
    expect(service).toBeTruthy();
  }));
});
