import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'feed-back',
  templateUrl: './feed-back.component.html',
  styleUrls: ['./feed-back.component.scss']
})
export class FeedBackComponent implements OnInit {
  @Input() volunteerName: string;

  constructor() { }

  ngOnInit() {
  }

}
