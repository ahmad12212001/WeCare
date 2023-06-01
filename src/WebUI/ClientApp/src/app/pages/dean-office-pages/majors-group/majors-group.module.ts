import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MajorsGroupComponent } from './majors-group.component';
import { SharedModule } from '@shared/shared.module';
import { MajorGroupsRoutingModule } from './majors-group.routing.module';
import { NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { MajorsGroupOperationComponent } from './majors-group-operation/majors-group-operation.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    NgxDatatableModule,
    NgbTooltipModule,
    MajorGroupsRoutingModule
  ],
  declarations: [MajorsGroupComponent, MajorsGroupOperationComponent]
})
export class MajorsGroupModule { }
