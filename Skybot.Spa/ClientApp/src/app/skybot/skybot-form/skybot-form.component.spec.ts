import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SkybotFormComponent } from './skybot-form.component';

describe('SkybotFormComponent', () => {
  let component: SkybotFormComponent;
  let fixture: ComponentFixture<SkybotFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SkybotFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SkybotFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
