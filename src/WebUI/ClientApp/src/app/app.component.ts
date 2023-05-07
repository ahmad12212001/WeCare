import { Component } from '@angular/core';
import { fromEvent } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  ngOnInit() {
    fromEvent(window, 'load').subscribe(() =>
     document.querySelector('#glb-loader')?.classList.remove('loaderShow'));
  }

  materialDualListSource: any[] = []
  destination = []

  constructor() { this.materialDualListSource = [{ name: "one" }, { name: "two" }, { name: "tree" }, { name: "four" }] }
}
