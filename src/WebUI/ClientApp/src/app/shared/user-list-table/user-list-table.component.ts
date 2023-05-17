import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { environment } from '@environment/environment';
import { FormBase } from '@shared/models/form-base';
import { PaginatedList } from '@shared/models/paginated-list';
import { UserDto } from '@shared/models/user-dto';
import { map, startWith, takeUntil } from 'rxjs';

@Component({
  selector: 'user-list-table',
  templateUrl: './user-list-table.component.html',
  styleUrls: ['./user-list-table.component.scss']
})
export class UserListTableComponent extends FormBase implements OnInit {
  @Input() title: string;
  users: PaginatedList<UserDto>;
  @Input() endPoint: string;
  pageSize: number = 10;
  pageNumber: number = 1;
  search = new FormControl('');

  constructor(private _http: HttpClient) {
    super();
  }

  ngOnInit() {
    this.getUsersPagination();

    this.search.valueChanges.pipe(startWith(''), map(value => {
      this.pageNumber = 1;
      this.getUsersPagination();
    })).subscribe()
  }

  getUsersPagination() {
    return this._http.get<PaginatedList<UserDto>>(`${environment.apiUrl}${this.endPoint}`, {
      params: {
        pageNumber: this.pageNumber,
        pageSize: this.pageSize,
        filter: this.search.value
      }
    }).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
      this.users = res;
    });
  }

  setPage(info: any) {
    this.getUsersPagination();
  }

}
