import { Component, OnInit } from '@angular/core';
import { FormBase } from '@shared/models/form-base';
import { Review } from '@shared/models/review';

@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.scss']
})
export class ReviewsComponent extends FormBase implements OnInit {
  reviews: Review[];
  constructor() {
    super()
  }

  ngOnInit() {
  }

}
