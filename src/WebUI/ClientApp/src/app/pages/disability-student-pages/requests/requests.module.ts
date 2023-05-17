import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RequestsComponent } from './requests.component';
import { RequestOperationComponent } from './request-operation/request-operation.component';
import { RequestsRoutingModule } from './requests.routing.module';
import { SharedModule } from '@shared/shared.module';
import { DropDownModule } from '@shared/modules/drop-down/drop-down.module';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { UserListTableModule } from '@shared/user-list-table/user-list-table.module';
import { FeedBackModule } from '@shared/modules/feed-back/feed-back.module';
import { FileUploadModule } from '@shared/modules/file-upload/file-upload.module';

@NgModule({
  imports: [
    CommonModule,
    RequestsRoutingModule,
    SharedModule,
    DropDownModule,
    NgxDatatableModule,
    UserListTableModule,
    FeedBackModule,
    FileUploadModule
  ],
  declarations: [RequestsComponent, RequestOperationComponent]
})
export class RequestsModule { }
