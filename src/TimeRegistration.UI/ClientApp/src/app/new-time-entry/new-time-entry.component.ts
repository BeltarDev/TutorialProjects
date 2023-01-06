import { DatePipe, formatDate } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { concat } from 'rxjs';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { TimeEntry } from '../ITimeEntry';
import * as moment from 'moment';


@Component({
  selector: 'app-new-time-entry',
  templateUrl: './new-time-entry.component.html',
  styleUrls: ['./new-time-entry.component.css']
})

export class NewTimeEntryComponent implements OnInit {
  model: NewTimeEntryModel = {
    title: undefined,
    description: undefined,
    option: undefined,
    startDate: undefined,
    startTimeHours: 0,
    startTimeMinutes: 0
  };

    @Output() timeEntrySubmitted = new EventEmitter<TimeEntry>();

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  async submit() {
    const momentDate = moment(this.model.startDate, 'DD-MM-YYYY');
    momentDate.add(this.model.startTimeHours, 'hours');
    momentDate.add(this.model.startTimeMinutes, 'minutes');

    try {
      const newTimeEntryFromApi = await this.http.post<TimeEntry>(this.baseUrl + 'api/Time',
        {
          startTime: momentDate,
          title: this.model.title,
          description: this.model.description
        }).toPromise();
      
      this.model = {
        title: undefined,
        description: undefined,
        option: undefined,
        startDate: undefined,
        startTimeHours: 0,
        startTimeMinutes: 0
      };

      this.timeEntrySubmitted.emit(newTimeEntryFromApi);
    } catch (error) {
      console.log('API Error: ', error);
    }
  }

  ngOnInit(): void {
  }

  useCurrentTime() {
    this.model.startDate = moment().startOf('day').format('DD-MM-YYYY');
    this.model.startTimeHours = moment().hour();
    this.model.startTimeMinutes = moment().minute();
  }

  useStartTime() {
    this.model.startDate = moment().startOf('day').format('DD-MM-YYYY');
    this.model.startTimeHours = 8;
    this.model.startTimeMinutes = 30;
  }

  useStopTime() {
    this.model.startDate = moment().startOf('day').format('DD-MM-YYYY');
    this.model.startTimeHours = 17;
    this.model.startTimeMinutes = 0;
  }
}

class NewTimeEntryModel {
  title: string | undefined;
  description: string | undefined;
  option: TimeEntryOption | undefined;
  startDate: string | undefined;
  startTimeHours: number = 0;
  startTimeMinutes: number = 0;
}

enum TimeEntryOption {
  CurrentTime = 'CurrentTime',
  DefaultStart = 'DefaultStart',
  DefaultEnd = 'DefaultEnd',
  Custom = 'Custom'
}

