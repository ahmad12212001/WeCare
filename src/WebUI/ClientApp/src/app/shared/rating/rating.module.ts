import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RatingComponent } from './rating.component';
import { NgbModalModule, NgbRatingModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  imports: [
    CommonModule,
    NgbRatingModule,
    NgbModalModule
  ],
  declarations: [RatingComponent],
  exports: [RatingComponent]
})
export class RatingModule { }
