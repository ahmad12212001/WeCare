import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RequestsComponent } from './requests.component';
import { RequestOperationComponent } from './request-operation/request-operation.component';
import { RequestsRoutingModule } from './requests.routing.module';
import { SharedModule } from '@shared/shared.module';
import { DropDownModule } from '@shared/modules/drop-down/drop-down.module';
import { UserListTableModule } from '@shared/user-list-table/user-list-table.module';
import { FeedBackModule } from '@shared/modules/feed-back/feed-back.module';
import { MaterialDialogModule } from '../materials/material-dialog/material-dialog.module';

@NgModule({
  imports: [
    CommonModule,
    RequestsRoutingModule,
    SharedModule,
    DropDownModule,
    UserListTableModule,
    FeedBackModule,
    MaterialDialogModule
  ],
  declarations: [RequestsComponent, RequestOperationComponent]
})
export class RequestsModule { }
