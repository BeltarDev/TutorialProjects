import { DatePipe, formatDate } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { concat } from 'rxjs';
import { TimeEntry } from '../ITimeEntry';
import * as moment from 'moment';

@Component({
  selector: 'app-new-time-entry',
  templateUrl: './new-time-entry.component.html',
  styleUrls: ['./new-time-entry.component.css']
})
export class NewTimeEntryComponent implements OnInit {
  model: NewTimeEntryModel = {
    title: '',
    option: undefined,
    startDate: new Date(),
    startTimeHours: 0,
    startTimeMinutes: 0
  };

  constructor() {
  }

  ngOnInit(): void {
  }

  useCurrentTime() {
    this.model.startDate = moment().utc().startOf('day').toDate();
    this.model.startTimeHours = moment().utc().hour();
    this.model.startTimeMinutes = moment().utc().minute();
  }

  useStartTime() {
    this.model.startDate = moment().utc().startOf('day').toDate();
    this.model.startTimeHours = 7;
    this.model.startTimeMinutes = 0;
  }

  useStopTime() {
    this.model.startDate = moment().utc().startOf('day').toDate();
    this.model.startTimeHours = 16;
    this.model.startTimeMinutes = 0;
  }

  submit() {
    console.log('Have to add thingy: ', this.model.title);
    console.log('have to add option > option selects datetime', this.model.option);

    // todo: call api with user given values


    this.model = {
      title: undefined,
      option: undefined,
      startDate: undefined,
      startTimeHours: 0,
      startTimeMinutes: 0
    };
  }
}

class NewTimeEntryModel {
  title: string | undefined;
  option: TimeEntryOption | undefined;
  startDate: Date | undefined;
  startTimeHours: number = 0;
  startTimeMinutes: number = 0;
}

enum TimeEntryOption {
  CurrentTime = 'CurrentTime',
  DefaultStart = 'DefaultStart',
  DefaultEnd = 'DefaultEnd',
  Custom = 'Custom'
}

function padTo2Digits(num: number) {
  return num.toString().padStart(2, '0');
}

function getCurrentTime() {
  var now = Date();
  return now;
}

function getDefaultStart(date: Date) {

  return (
    [
      date.getFullYear(),
      padTo2Digits(date.getMonth() + 1),
      padTo2Digits(date.getDate()),
    ].join('-') + '' +
    [
      padTo2Digits(date.setHours(8)),
      padTo2Digits(date.setMinutes(30)),
      padTo2Digits(date.setSeconds(0)),
    ].join(':')
  );
}

  function getDefaultEnd(date: Date) {
    return (
      [
        date.getFullYear(),
        padTo2Digits(date.getMonth() + 1),
        padTo2Digits(date.getDate()),
      ].join('-') + '' +
      [
        padTo2Digits(date.setHours(17)),
        padTo2Digits(date.setMinutes(0)),
        padTo2Digits(date.setSeconds(0)),
      ].join(':')
    );
  }

