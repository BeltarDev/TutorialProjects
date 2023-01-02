import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewTimeEntryComponent } from './new-time-entry.component';

describe('NewTimeEntryComponent', () => {
  let component: NewTimeEntryComponent;
  let fixture: ComponentFixture<NewTimeEntryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NewTimeEntryComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NewTimeEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
