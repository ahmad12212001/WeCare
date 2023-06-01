import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { User } from '@app/pages/models/user';
import { UsersService } from '@app/pages/services/users.service';
import { FormBase } from '@shared/models/form-base';
import { PaginatedList } from '@shared/models/paginated-list';
import { AlertService } from '@shared/modules/alert/alert.service';
import { ColumnMode } from '@swimlane/ngx-datatable';
import { map, startWith, takeUntil } from 'rxjs';

@Component({
  selector: 'users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent extends FormBase implements OnInit {
  rows: PaginatedList<User>;
  pageSize: number = 10;
  pageNumber: number = 1;
  name = new FormControl('');
  ColumnMode = ColumnMode;

  constructor(private _usersService: UsersService, private _alertService: AlertService) {
    super();
  }

  ngOnInit() {
    this.getUserPagination();

    this.name.valueChanges.pipe(startWith(''), map(value => {
      this.pageNumber = 1;
      this.getUserPagination();
    })).subscribe()
  }

  setPage(pageInfo) {
    this.pageNumber = pageInfo.offset;
    this.getUserPagination();
  }

  getUserPagination() {
    this._usersService.getUsersPagination(this.pageSize, this.pageNumber, this.name.value).subscribe(pagedData => {
      this.rows = pagedData
    });
  }

  deleteUser(id: string) {
    this._alertService.modalSize = "md";
    this._alertService.warn('Are you sure you want to delete this Course !!!', this.rows.items.find(i => i.id == id).firstName, {
      autoClose: false,
      showCloseButton: true,
      actionMessage: 'Yes',
      closeButtonText: 'No',
      cssClass: 'btn-white',
      closeButtonCss: 'btn-primary'
    }
    );
    this._alertService.submitAction$.pipe(takeUntil(this.onDestroy$)).subscribe(res => {
      if (res) {
        this._usersService.deleteUser(id).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
          if (res) {
            this.getUserPagination();
          }
        })
      }
    });
  }
}
