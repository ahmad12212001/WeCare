import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExamsComponent } from './exams.component';
import { ExamsRoutingModule } from './exams.routing.module';
import { SharedModule } from '../../../shared/shared.module';
import { ExamOperationComponent } from './exam-operation/exam-operation.component';
import { DropDownModule } from '@shared/modules/drop-down/drop-down.module';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
@NgModule({
  imports: [
    CommonModule,
    ExamsRoutingModule,
    SharedModule,
    DropDownModule,
    NgxDatatableModule
  ],
  declarations: [ExamsComponent, ExamOperationComponent]
})
export class ExamsModule { }
