import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExamsComponent } from './exams.component';
import { ExamsRoutingModule } from './exams.routing.module';

@NgModule({
  imports: [
    CommonModule,
    ExamsRoutingModule
  ],
  declarations: [ExamsComponent]
})
export class ExamsModule { }
