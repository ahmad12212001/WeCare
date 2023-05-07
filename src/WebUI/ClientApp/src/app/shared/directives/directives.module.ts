import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FocusDirective } from './focus.directive';
import { DisableAutofillDirective } from './disableAutofill.directive';
import { ClickStopPropagationDirective } from './click-stop-propagation.directive';
import { ValidationDirective } from './validation.directive';
import { DynamicControlDirective } from './dynamic-control.directive';
import { DigitOnlyModule } from './number-lib/digit-only.module';
import { AutofocusDirective } from './autofocus.directive';



@NgModule({
  imports: [
    CommonModule,
    DigitOnlyModule
  ],

  declarations: [FocusDirective,
    DisableAutofillDirective,
    ClickStopPropagationDirective,
    ValidationDirective,
    DynamicControlDirective,
    AutofocusDirective
  ],
  exports: [FocusDirective, DisableAutofillDirective, ClickStopPropagationDirective,
    ValidationDirective,DynamicControlDirective,DigitOnlyModule,AutofocusDirective]
})
export class DirectivesModule { }
