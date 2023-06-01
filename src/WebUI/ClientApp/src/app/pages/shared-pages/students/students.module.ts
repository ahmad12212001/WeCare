import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudentsComponent } from './students.component';
import { StudentOperationComponent } from './student-operation/student-operation.component';
import { DropDownModule } from '@shared/modules/drop-down/drop-down.module';
import { NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { SharedModule } from '@shared/shared.module';
import { StudentsRoutingModule } from './students.routing.module';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { StudentCourseOperationComponent } from './student-course-operation/student-course-operation.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    NgxDatatableModule,
    NgbTooltipModule,
    DropDownModule,
    StudentsRoutingModule,
    NgMultiSelectDropDownModule.forRoot()
  ],
  declarations: [StudentsComponent, StudentOperationComponent, StudentCourseOperationComponent]
})
export class StudentsModule { }
