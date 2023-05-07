import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DropDownComponent } from './drop-down.component';
import { ClickOutsideModule } from 'ng-click-outside';

@NgModule({
  imports: [
    CommonModule,
    ClickOutsideModule
  ],
  declarations: [DropDownComponent],
  exports: [DropDownComponent]
})
export class DropDownModule { }
