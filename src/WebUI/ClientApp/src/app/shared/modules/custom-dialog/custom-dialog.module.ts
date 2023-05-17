import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomDialogComponent } from './custom-dialog.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [CustomDialogComponent],
  exports: [CustomDialogComponent],
  entryComponents: [CustomDialogComponent]
})
export class CustomDialogModule { }
