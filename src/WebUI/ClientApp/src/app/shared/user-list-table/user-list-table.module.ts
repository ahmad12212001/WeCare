import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserListTableComponent } from './user-list-table.component';
import { NgbModule, NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { RatingModule } from '@shared/rating/rating.module';
import { SharedModule } from '@shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    NgbPaginationModule,
    NgbModule,
    RatingModule,
    SharedModule
  ],
  declarations: [UserListTableComponent],
  exports: [UserListTableComponent]
})
export class UserListTableModule { }
