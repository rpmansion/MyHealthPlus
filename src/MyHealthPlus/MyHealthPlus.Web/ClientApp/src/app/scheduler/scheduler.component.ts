import { Component, OnInit, ChangeDetectionStrategy, ViewEncapsulation, Inject, TemplateRef, ViewChild } from '@angular/core';

import {
  CalendarEvent,
  CalendarView,
  CalendarEventAction
} from 'angular-calendar';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { format, addHours, startOfDay, subDays, addDays, endOfMonth, isSameMonth, isSameDay } from 'date-fns';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { RegisterModalComponent } from '../account/register-modal/register-modal.component';
import { ScheduledModalComponent } from './scheduled-modal/scheduled-modal.component';

const colors: any = {
  red: {
    primary: '#ad2121',
    secondary: '#FAE3E3'
  },
  blue: {
    primary: '#1e90ff',
    secondary: '#D1E8FF'
  },
  yellow: {
    primary: '#e3bc08',
    secondary: '#FDF1BA'
  }
};

@Component({
  selector: 'app-scheduler',
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None,
  templateUrl: './scheduler.component.html',
  styleUrls: ['./scheduler.component.scss']
})
export class SchedulerComponent implements OnInit {
  form: FormGroup;

  view: CalendarView = CalendarView.Month;
  viewDate: Date = new Date();
  refresh: Subject<any> = new Subject();

  events: CalendarEvent[] = [];

  constructor(private modal: NgbModal,
    private http: HttpClient,
    private formBuilder: FormBuilder,
    @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit() {
    this.createForm();
    this.getPatientAppointments(1).subscribe(
      response => {
        response.forEach((item: any) => {
          this.events.push({
            start: this.getStartHour(item.date, item.time),
            title: '',
            color: colors.red
          });
          this.refresh.next();
        });
      }
    );
  }

  private createForm() {
    this.form = this.formBuilder.group({
      checkupType: '',
      appointmentDate: '',
      appointmentTime: '',
      note: ''
    });
  }

  getStartHour(date: any, time: any) {
    const day = startOfDay(new Date(date));
    const hour = new Date(time).getUTCHours();

    return addHours(day, hour);
  }

  getListOfTime() {
    const times: { id: string, name: string }[] = [];
    for (let index = 8; index < 18; index++) {
      const name = format(addHours(startOfDay(new Date()), index), 'hh:mm a');
      times.push({ id: index.toString(), name: name });
    }
    return times;
  }

  getCheckupType() {
    return [
      { id: '1', name: 'General' },
      { id: '2', name: 'Skin Cancer' }
    ];
  }

  handleFormSubmit(data: any) {
    this.createAppointment(data).subscribe(
      response => {
        console.log(response);
      }
    );
  }

  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    if (events.length) {
      events.forEach(item => {
        item.title = 'Scheduled';
      });
      const modalRef = this.modal.open(ScheduledModalComponent);
      modalRef.componentInstance.viewDate = date;
      modalRef.componentInstance.modalData = events;
    }
  }

  // TODO : move this to service
  getPatientAppointments(data: any): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}api/appointment/patients`);
  }

  // TODO : move this to service
  createAppointment(data: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}api/appointment/create`, data);
  }
}
