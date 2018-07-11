import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ToursComponent } from './tours/tours.component';
import { AboutComponent } from './about';
import { TourDetailComponent } from './tours/tour-detail/tour-detail.component';
import { TourUpdateComponent } from './tours/tour-update/tour-update.component';
import { TourAddComponent } from './tours/tour-add/tour-add.component';
import { ShowAddComponent } from './tours/shows/show-add/show-add.component';

const routes: Routes = [
  { path: '', redirectTo: 'tours', pathMatch: 'full' },
  { path: 'tours', component: ToursComponent },
  { path: 'about', component: AboutComponent },
  { path: 'tours/:tourId', component: TourDetailComponent },
  { path: 'tour-update/:tourId', component: TourUpdateComponent },
  { path: 'tour-add', component: TourAddComponent },
  { path: 'tours/:tourId/show-add', component: ShowAddComponent }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
