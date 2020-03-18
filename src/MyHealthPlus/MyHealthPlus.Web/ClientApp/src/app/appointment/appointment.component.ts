import {
  Component,
  ChangeDetectionStrategy,
  ViewEncapsulation,
  OnInit,
  Inject
} from '@angular/core';

import {
  CalendarEvent,
  CalendarMonthViewBeforeRenderEvent,
  CalendarWeekViewBeforeRenderEvent,
  CalendarDayViewBeforeRenderEvent,
  CalendarView,
  CalendarEventAction
} from 'angular-calendar';

import {
  startOfDay,
  addHours
} from 'date-fns';
import { Observable, Subject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { map } from 'rxjs/operators';

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
  selector: 'app-appointment',
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None,
  templateUrl: './appointment.component.html',
  styleUrls: ['./appointment.component.scss']
})
export class AppointmentComponent implements OnInit {
  viewDate: Date = new Date();
  view: CalendarView = CalendarView.Day;

  refresh: Subject<any> = new Subject();

  events: CalendarEvent[] = [];
  actions: CalendarEventAction[] = [
    {
      label: '<i class="fa fa-fw fa-times"></i>',
      a11yLabel: 'Edit',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.handleCalendarClickEvent('Edited', event);
        console.log('went here');
      }
    }
  ];

  isAuthenticated: Observable<boolean>;
  userName: Observable<string>;

  constructor(private http: HttpClient,
    private authorizeService: AuthorizeService,
    @Inject('BASE_URL') private baseUrl: string) { }

  ngOnInit() {
    this.getDoctorAppointments(1).subscribe(
      response => {
        console.log(response);
        response.forEach(item => {
          this.events.push({
            start: this.getStartHour(item.date, item.time),
            title: this.getTitle(item.checkupType, 'Robert Mansion'),
            color: colors.yellow,
            actions: this.actions
          });
          this.refresh.next();
        });
      }
    );

    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.userName = this.authorizeService.getUser().pipe(map(u => u && u.name));
  }

  getStartHour(date: any, time: any) {
    const day = startOfDay(new Date(date));
    const hour = new Date(time).getUTCHours();

    return addHours(day, hour);
  }

  getTitle(id: number, name: string) {
    return `<b>${this.getCheckupTypeName(id)}:</b> ${name}`;
  }

  getCheckupTypeName(id: number) {
    switch (id) {
      case 1:
        return 'General Checkup';
      case 2:
        return 'Skin Cancer Checkup';
    }
  }

  handleCalendarClickEvent(action: string, event: CalendarEvent): void {
    console.log(action, event);
    // this.modalData = { event, action };
    // this.modal.open(this.modalContent, { size: 'lg' });
  }

  // TODO : move this to service
  getDoctorAppointments(data: any): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}api/appointment/doctor/${data}`, data);
  }

}
