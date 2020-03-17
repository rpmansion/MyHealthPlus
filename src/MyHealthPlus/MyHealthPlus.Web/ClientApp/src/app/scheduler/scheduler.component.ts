import { Component, OnInit, ChangeDetectionStrategy, ViewEncapsulation, Inject } from '@angular/core';

import {
  CalendarEvent,
  CalendarMonthViewBeforeRenderEvent,
  CalendarView
} from 'angular-calendar';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { format, addHours, startOfDay } from 'date-fns';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-scheduler',
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None,
  templateUrl: './scheduler.component.html',
  styleUrls: ['./scheduler.component.scss']
})
export class SchedulerComponent implements OnInit {
  view: CalendarView = CalendarView.Month;
  viewDate: Date = new Date();
  events: CalendarEvent[] = [];

  form: FormGroup;

  constructor(private http: HttpClient,
    private formBuilder: FormBuilder,
    @Inject('BASE_URL') private baseUrl: string) { }

    ngOnInit() {
      this.createForm();
    }

    private createForm() {
      this.form = this.formBuilder.group({
        checkupType: '',
        appointmentDate: '',
        appointmentTime: '',
        note: ''
      });
    }

  getListOfTime() {
    const times: {id: string, name: string}[] = [];
    for (let index = 8; index < 18; index++) {
      const name = format(addHours(startOfDay(new Date()), index), 'hh:mm a');
      times.push({id: index.toString(), name: name });
    }
    return times;
  }

  getCheckupType() {
    return [
      {id: '1', name: 'General' },
      {id: '2', name: 'Skin Cancer'}
    ];
  }

  beforeMonthViewRender(renderEvent: CalendarMonthViewBeforeRenderEvent): void {
    renderEvent.body.forEach(day => {
      const dayOfMonth = day.date.getDate();
      if (dayOfMonth > 5 && dayOfMonth < 10 && day.inMonth) {
        day.cssClass = 'bg-pink';
      }
    });
  }

  handleFormSubmit(data: any) {
    this.createAppointment(data).subscribe(
      response => {
        console.log(response);
      }
    );
  }

  // TODO : move this to service
  createAppointment(data: any): Observable<any> {
    const headerOptions = new HttpHeaders();
    headerOptions.set('Content-Type', 'application/json');
    return this.http.post<any>(`${this.baseUrl}api/appointment/create`, data);
    // return this.http.post<any>(`${this.baseUrl}api/appointment/create`, data)
  }

}
