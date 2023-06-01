import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MajorsComponent } from './majors.component';
import { SharedModule } from '@shared/shared.module';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { DropDownModule } from '@shared/modules/drop-down/drop-down.module';
import { MajorsOperationComponent } from './majors-operation/majors-operation.component';
import { MajorsRoutingModule } from './majors.routing.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    NgxDatatableModule,
    NgbTooltipModule,
    DropDownModule,
    MajorsRoutingModule
  ],
  declarations: [MajorsComponent, MajorsOperationComponent]
})
export class MajorsModule { }
