import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CalendarEvent, CalendarView } from 'angular-calendar';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-scheduled-modal',
  templateUrl: './scheduled-modal.component.html',
  styleUrls: ['./scheduled-modal.component.scss']
})
export class ScheduledModalComponent implements OnInit {
  viewDate: Date;
  view: CalendarView = CalendarView.Day;

  refresh: Subject<any> = new Subject();

  modalData: CalendarEvent[];

  constructor(private activeModal: NgbActiveModal) { }

  ngOnInit() {
    console.log(this.viewDate);
    console.log(this.modalData);
  }

  close() {
    this.activeModal.close('Modal Closed');
  }
}
