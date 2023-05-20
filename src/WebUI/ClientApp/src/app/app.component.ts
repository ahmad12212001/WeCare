import { Component } from '@angular/core';
import { fromEvent } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  ngOnInit() {
 
  }

  materialDualListSource: any[] = []
  destination = []

  constructor() { this.materialDualListSource = [{ name: "one" }, { name: "two" }, { name: "tree" }, { name: "four" }] }
}
