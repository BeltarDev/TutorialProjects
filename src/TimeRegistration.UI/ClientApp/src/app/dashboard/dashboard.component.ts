import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { concat } from 'rxjs';
import { TimeEntry } from '../ITimeEntry';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  pageSize = 100;
  pageNumber = 1;
  
  selectedTimeEntry?: TimeEntry;
  timeEntries: TimeEntry[] = [];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.getData();
  }

  getData(){
    this.http.get<TimeEntry[] >(this.baseUrl + `api/Time?pageSize=${this.pageSize}&pageNumber=${this.pageNumber}`)
      .subscribe(result => {
        this.timeEntries = result;
        console.log("Received: ", result);
      }, error => console.error(error));
  }
  
  getPreviousPage(){
      
    if (this.pageNumber <= 1 ){
      return;
    }

    this.pageNumber --;
    this.getData()
  }

  canGetNextPage(){
    if (this.timeEntries.length < this.pageSize ){
      return false;
    }

    return true;
  }

  getNextPage(){
    // caculate current page
    // decide if can get next page
    // actually get next page

    if (this.timeEntries.length < this.pageSize ){
      return;
    }

    this.pageNumber ++;
    this.getData()
    
  }


  onSelect(timeEntry: TimeEntry): void {
    console.log('New selected ding: ', timeEntry)
    this.selectedTimeEntry = timeEntry;
  }
}
