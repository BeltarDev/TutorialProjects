import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { NewTimeEntryComponent } from './new-time-entry/new-time-entry.component';
import { CommonModule } from '@angular/common';
import { MomentModule } from 'ngx-moment';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,

    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    DashboardComponent,
    NewTimeEntryComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    //CommonModule,
    HttpClientModule,
    FormsModule,
    MomentModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'dashboard', component: DashboardComponent},
      { path: 'new-time-entry', component: NewTimeEntryComponent},
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
