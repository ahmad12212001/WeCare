import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReviewsComponent } from './reviews.component';
import { RateDirective } from './rate.directive';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [	ReviewsComponent,
      RateDirective
   ],
  exports: [ReviewsComponent]
})
export class ReviewsModule { }
