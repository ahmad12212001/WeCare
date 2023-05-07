import { Directive, ElementRef, Input, OnChanges, OnDestroy } from '@angular/core';
import { FormArray, FormControl, FormGroup } from '@angular/forms';
import { ReplaySubject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Directive({
  selector: '[validation]'
})
export class ValidationDirective implements OnChanges, OnDestroy {
  @Input('form') form!: FormGroup;
  @Input('submitted') submitted!: boolean;
  onDestroy$ = new ReplaySubject<void>();
  constructor(private el: ElementRef) {

  }
  ngOnDestroy(): void {
    this.onDestroy$.next();
    this.onDestroy$.complete();
  }
  ngOnChanges(): void {
    if (this.submitted)
      this.validateForm();
  }

  validateForm() {

    let inputs = this.el.nativeElement.querySelectorAll('input,textarea');
    let inputsLength = inputs.length;
    let formKeys = Object.keys(this.form.controls);
    let formKeysLength = formKeys.length;
    for (let index = 0; index < formKeysLength; index++) {
      let key = formKeys[index];
      let control = this.form.controls[key];
      if (control instanceof FormArray) {
        if (control instanceof FormArray) {
          for (const control1 of control.controls) {
            if (control1 instanceof FormControl) {
              this.validate(control1, inputs, key);
            }
            if (control1 instanceof FormGroup) {
              this.validateSubForm(control1);
            }
          }
        }
      }
      if (control instanceof FormGroup) {
        this.validateSubForm(control)
      }
    
      if ((control.invalid || control.errors) && control.validator) {
        for (let j = 0; j < inputsLength; j++) {
          let inputByFormControl = inputs[j].getAttribute("formcontrolname");
          let inputById = inputs[j].getAttribute("id");
          if (inputByFormControl === key || inputById === key) {
            inputs[j].parentElement.classList.add('err');
            control.valueChanges.pipe(takeUntil(this.onDestroy$)).subscribe(res => {
              if (res && control.errors === null) {
                inputs[j].parentElement.classList.remove('err');

              } else {
                inputs[j].parentElement.classList.add('err');
              }
            });
          }

        }
      }

    }

  }
  validateSubForm(form: FormGroup) {

    let inputs = this.el.nativeElement.querySelectorAll('input');

    let formKeys = Object.keys(form.controls);
    let formKeysLength = formKeys.length;
    for (let index = 0; index < formKeysLength; index++) {
      let key = formKeys[index];
      let control = form.controls[key];
      if (control instanceof FormArray) {
        for (const control1 of control.controls) {
          if (control1 instanceof FormControl) {
            this.validate(control1, inputs, key);
          }
          if (control1 instanceof FormGroup) {
            this.validateSubForm(control1);
          }
        }
      }
      if (control instanceof FormGroup) {
        this.validateSubForm(control);
      }
      if (control instanceof FormControl) {
        this.validate(control, inputs, key);
      }

    }

  }

  validate(control: FormControl, inputs: any, key: string) {
    let inputsLength = inputs.length;
    if ((control.invalid || control.errors) && control.validator) {
      for (let j = 0; j < inputsLength; j++) {
        let inputByFormControl = inputs[j].getAttribute("formcontrolname");
        let inputById = inputs[j].getAttribute("id");
        if (!inputByFormControl) {
          inputByFormControl = inputs[j].getAttribute(inputById);
        }
        if (inputByFormControl === key || inputById === key || inputById.includes(key)) {
          if (!inputs[j].value) {
            inputs[j].parentElement.classList.add('err');
            control.valueChanges.pipe(takeUntil(this.onDestroy$)).subscribe(res => {
              if (res && control.errors === null) {
                inputs[j].parentElement.classList.remove('err');

              } else {
                inputs[j].parentElement.classList.add('err');
              }
            });
          }

        }

      }
    }
  }
}
