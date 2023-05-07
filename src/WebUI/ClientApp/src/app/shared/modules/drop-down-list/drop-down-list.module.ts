import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DropDownListComponent } from './drop-down-list.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ClickOutsideModule } from 'ng-click-outside';
import { DirectivesModule } from '@shared/directives';
import { PipesModule } from '@shared/pipes/pipes.module';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ClickOutsideModule,
    DirectivesModule,
    PipesModule
  ],
  declarations: [DropDownListComponent],
  exports: [DropDownListComponent]
})
export class DropDownListModule { }
