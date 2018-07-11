import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { AboutComponent } from './about/about.component';
import { ToursComponent } from './tours/tours.component';
import { ShowsComponent } from './tours/shows/shows.component';
import { TourAddComponent } from './tours/tour-add/tour-add.component';
import { TourDetailComponent } from './tours/tour-detail/tour-detail.component';
import { TourUpdateComponent } from './tours/tour-update/tour-update.component';
import { AppRoutingModule } from './/app-routing.module';
import { ShowAddComponent } from './tours/shows/show-add/show-add.component';

@NgModule({
  declarations: [
    AppComponent,
    AboutComponent,
    ToursComponent,
    ShowsComponent,
    TourAddComponent,
    TourDetailComponent,
    TourUpdateComponent,
    ShowAddComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
