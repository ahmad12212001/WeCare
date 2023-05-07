
import { Directive, ElementRef, Input, OnChanges,  } from '@angular/core';

@Directive({
  selector: '[backgroundImage]'
})
export class BackgroundImageDirective implements OnChanges {

  @Input() image!: ArrayBuffer | string | null;
  private htmlElement!: HTMLElement;

  constructor(el: ElementRef) {
    this.htmlElement = el.nativeElement;
  }
  ngOnChanges(): void {
    if (this.image)
      if (typeof this.image === 'string') {
        this.htmlElement.style.backgroundImage = 'url(' + this.image + ')';
      }
      else {
        this.htmlElement.style.backgroundImage = 'url(' + this.image + ')';
      }
  }



}
