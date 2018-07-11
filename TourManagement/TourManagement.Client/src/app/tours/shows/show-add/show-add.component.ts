import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { Show } from '../shared/show.model';
import { ShowService } from '../shared/show.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-show-add',
  templateUrl: './show-add.component.html',
  styleUrls: ['./show-add.component.css']
})
export class ShowAddComponent implements OnInit, OnDestroy {

  private sub: Subscription;
  private tourId: string;
  shows: Show[];

  constructor(private showService: ShowService,
    private route: ActivatedRoute) { }

  ngOnInit() {
    // get route data (tourId)
    this.sub = this.route.params.subscribe(
      params => {
        this.tourId = params['tourId'];

        // load tour
        this.showService.getShows(this.tourId)
          .subscribe(shows => {
            this.shows = shows;
          });
      }
    );
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

}
