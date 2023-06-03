import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.scss']
})
export class LandingPageComponent implements OnInit {

  constructor() { }

  ngOnInit() {

    const isReloaded = localStorage.getItem('reloaded');
    if (isReloaded === 'false') {
      localStorage.setItem('reloaded', "true");
      location.reload();
    }
  }

}
