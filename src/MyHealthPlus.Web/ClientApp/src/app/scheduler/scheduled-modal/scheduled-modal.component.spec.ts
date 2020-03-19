import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ScheduledModalComponent } from './scheduled-modal.component';

describe('ScheduledModalComponent', () => {
  let component: ScheduledModalComponent;
  let fixture: ComponentFixture<ScheduledModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ScheduledModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ScheduledModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
