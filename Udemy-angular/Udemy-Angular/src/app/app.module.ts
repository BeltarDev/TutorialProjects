import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { ProductsComponent } from './products.component';
import { ProductComponent } from './product/product.component';
import { ProductsService } from './products.service';
import { HomeComponent } from './home.component';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app/app.component';


@NgModule({
  declarations: [
    ProductsComponent,
    ProductComponent,
    HomeComponent,
    AppComponent
    
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule
  ],
  providers: [
    ProductsService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
