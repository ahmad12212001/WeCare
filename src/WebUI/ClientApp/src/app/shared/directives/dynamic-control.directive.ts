import { Directive, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Directive({
  selector: '[dynamicControl]'
})
export class DynamicControlDirective implements OnInit {
  @Input('form') form!: FormGroup;
  @Input('controlName') controlName!: string
  @Input('isRequired') isRequired!: boolean;
  @Input('isDisabled') isDisabled!: boolean;
  @Input('value') value: any;
  constructor() { }
  ngOnInit(): void {
    let exist = this.form.get(this.controlName);
    if (!exist) {
      if (this.isRequired) {
        this.form.addControl(this.controlName, new FormControl({ value: this.value ? this.value : null, disabled: this.isDisabled }, Validators.required));
      } else {
        this.form.addControl(this.controlName, new FormControl({ value: this.value ? this.value : null, disabled: this.isDisabled }))

      }

    } else {
      if (this.isRequired) {
        this.form.controls[this.controlName].setValidators(Validators.required);
      }
      this.form.controls[this.controlName].updateValueAndValidity();
    }

  }

}
