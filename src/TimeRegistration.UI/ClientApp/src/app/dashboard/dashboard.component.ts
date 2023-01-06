import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { TimeEntry } from '../ITimeEntry';
import { ClrDatagridStateInterface, ClrLoadingState } from "@clr/angular";
import { tap } from 'rxjs/operators';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})

export class DashboardComponent {
  totalCount: number = 0;
  pageSize = 50;
  pageNumber = 1;

  loading: boolean = true;

  selectedTimeEntry?: TimeEntry;
  timeEntries: TimeEntry[] = [];
  openModal = false;
  openNewModal = false;
  deleteBtnState: ClrLoadingState = ClrLoadingState.DEFAULT;
  gridState: ClrDatagridStateInterface<any> | undefined;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.getData();
    /*  this.deleteEntry();*/
  }

  async getData(sort?: string) {
    let url = this.baseUrl + `api/Time?pageSize=${this.pageSize}&pageNumber=${this.pageNumber}`;

    if (sort) {
      url = url + `&orderBy=${sort}`;
    }

    const result = await this.http.get<any>(url).toPromise();
    this.timeEntries = result.records;
    this.totalCount = result.totalCount;
    console.log("Received: ", result);
  }
  
  async timeEntrySubmitted(timeEntry : TimeEntry) {
    
    this.openNewModal = false;
    await this.refresh(this.gridState);
  }
  
  openNewTimeEntryModal() {
    this.openNewModal = true;
  }

  closeDeleteModal() {
    this.selectedTimeEntry = undefined;
    this.openModal = false;
  }

  openDeleteModal(timeEntry: TimeEntry) {
    this.selectedTimeEntry = timeEntry;
    this.openModal = true;
  }

  async deleteSelectedTimeEntry() {
    this.openModal = false;
    this.deleteBtnState = ClrLoadingState.LOADING;
    await this.deleteEntry(this.selectedTimeEntry!);
    this.deleteBtnState = ClrLoadingState.DEFAULT;
    this.selectedTimeEntry = undefined;
  }

  async deleteEntry(timeEntry: TimeEntry) {
    await this.http.delete(this.baseUrl + `api/Time?id=${timeEntry.id}`)
      .toPromise();
    await this.getData();
  }

  async refresh(state?: ClrDatagridStateInterface ) {
    console.log(state);
    try {
      this.loading = true;

      this.pageSize = state?.page?.size ?? 50;
      this.pageNumber = state?.page?.current ?? 1;
      this.gridState = state;

      let sort = undefined;

      if (state?.sort) {
        // state? has sort info
        // what to do?
        const fieldName = state?.sort.by.toString(); // 'id' / 'title' / 'description'
        const isReversed = state?.sort.reverse; // true / false

        if (isReversed === true) {
          sort = fieldName + '_desc';
        }
        else {
          sort = fieldName;
        }
      }
      await this.getData(sort);
    } catch (error) {
      console.log('Error: ', error);
    }

    this.loading = false;
    console.log('loading complete');
  }

  getPreviousPage() {

    if (this.pageNumber <= 1) {
      return;
    }

    this.pageNumber--;
    this.getData();
  }

}
