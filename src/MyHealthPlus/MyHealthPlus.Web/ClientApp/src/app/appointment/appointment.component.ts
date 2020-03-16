import {
  Component,
  ChangeDetectionStrategy,
  ViewEncapsulation,
  OnInit
} from '@angular/core';

import {
  CalendarEvent,
  CalendarMonthViewBeforeRenderEvent,
  CalendarWeekViewBeforeRenderEvent,
  CalendarDayViewBeforeRenderEvent,
  CalendarView
} from 'angular-calendar';

import {
  startOfDay,
  subDays,
  addDays,
  endOfMonth,
  addHours
} from 'date-fns';

@Component({
  selector: 'app-appointment',
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None,
  templateUrl: './appointment.component.html',
  styleUrls: ['./appointment.component.scss']
})
export class AppointmentComponent implements OnInit {
  view: CalendarView = CalendarView.Day;

  viewDate: Date = new Date();

  events: CalendarEvent[] = [
    {
      start: addHours(startOfDay(new Date()), 8),
      end: addHours(startOfDay(new Date()), 9),
      title: 'A draggable and resizable event',
      // color: colors.yellow,
      // actions: this.actions
    },
    {
      start: addHours(startOfDay(new Date()), 9),
      end: addHours(startOfDay(new Date()), 10),
      title: 'A draggable and resizable event',
      // color: colors.yellow,
      // actions: this.actions
    },
    {
      start: addHours(startOfDay(new Date()), 10),
      end: addHours(startOfDay(new Date()), 11),
      title: 'A draggable and resizable event',
      // color: colors.yellow,
      // actions: this.actions
    }
  ];

  constructor() { }

  ngOnInit() {
  }

  beforeDayViewRender(renderEvent: CalendarDayViewBeforeRenderEvent) {
    renderEvent.hourColumns.forEach(hourColumn => {
      hourColumn.hours.forEach(hour => {
        hour.segments.forEach(segment => {
          if (segment.date.getHours() >= 2 && segment.date.getHours() <= 5) {
            segment.cssClass = 'bg-pink';
          }
        });
      });
    });
  }

}
