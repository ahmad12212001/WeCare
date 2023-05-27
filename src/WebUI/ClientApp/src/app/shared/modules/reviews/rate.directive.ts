import { Directive, ElementRef, Input, OnInit } from '@angular/core';

@Directive({
  selector: '[rate]'
})
export class RateDirective implements OnInit {

  @Input() rating: number;

  constructor(private _el: ElementRef) { }

  ngOnInit(): void {
    this._el.nativeElement.style.width = this.calculateRate();
  }

  calculateRate() {
    const starTotal = 5;
    const starPercentage = this.rating / starTotal * 100;
    return starPercentage + '%';
  }

}
