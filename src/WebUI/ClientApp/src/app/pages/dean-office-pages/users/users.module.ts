import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UsersComponent } from './users.component';
import { UsersRoutingModule } from './users.routing.module';
import { SharedModule } from '@shared/shared.module';
import { DropDownModule } from '@shared/modules/drop-down/drop-down.module';
import { UserOperationComponent } from './user-operation/user-operation.component';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ChangePasswordComponent } from './change-password/change-password.component';

@NgModule({
  imports: [
    CommonModule,
    UsersRoutingModule,
    SharedModule,
    DropDownModule,
    NgxDatatableModule
  ],
  declarations: [UsersComponent, UserOperationComponent, ChangePasswordComponent]
})
export class UsersModule { }
