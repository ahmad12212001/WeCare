import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialDialogComponent } from './material-dialog.component';
import { PipesModule } from '@shared/pipes/pipes.module';

@NgModule({
  imports: [
    CommonModule,
    PipesModule
  ],
  declarations: [MaterialDialogComponent],
  exports: [MaterialDialogComponent],
  entryComponents: [MaterialDialogComponent]
})
export class MaterialDialogModule { }
