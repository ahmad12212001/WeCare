import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialsComponent } from './materials.component';
import { MaterialOperationComponent } from './material-operation/material-operation.component';
import { DropDownModule } from '@shared/modules/drop-down/drop-down.module';
import { FileUploadModule } from '@shared/modules/file-upload/file-upload.module';
import { SharedModule } from '@shared/shared.module';
import { MaterialsRoutingModule } from './materials.routing.module';
import { PipesModule } from '@shared/pipes/pipes.module';
import { NgbAccordionModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  imports: [
    CommonModule,
    FileUploadModule,
    SharedModule,
    DropDownModule,
    MaterialsRoutingModule,
    PipesModule,
    NgbAccordionModule
  ],
  declarations: [MaterialsComponent, MaterialOperationComponent]
})
export class MaterialsModule { }
