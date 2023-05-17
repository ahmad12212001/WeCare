import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AlertComponent } from './alert.component';

import { ReactiveFormsModule } from '@angular/forms';
import { AlertService } from './alert.service';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  declarations: [AlertComponent],
  exports: [AlertComponent],
  providers: [AlertService]
})
export class AlertModule { }
