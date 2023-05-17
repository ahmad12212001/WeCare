import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoursesComponent } from './courses.component';
import { SharedModule } from '../../../shared/shared.module';
import { DropDownModule } from '@shared/modules/drop-down/drop-down.module';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { CourseOperationComponent } from './course-operation/course-operation.component';
import { CoursesRoutingModule } from './courses.routing.module';
import { NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  imports: [
    CommonModule,
    CoursesRoutingModule,
    SharedModule,
    DropDownModule,
    NgxDatatableModule,
    NgbTooltipModule
  ],
  declarations: [CoursesComponent, CourseOperationComponent]
})
export class CoursesModule { }
