import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'rating',
  templateUrl: './rating.component.html',
  styleUrls: ['./rating.component.scss']
})
export class RatingComponent implements OnInit {
  @Input() rate: number = 0;

  constructor() { }

  ngOnInit() {
  }

}
