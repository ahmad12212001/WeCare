import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MajorGroup } from '@app/pages/models/major-group';
import { MajorGroupService } from '@app/pages/services/major-group.service';
import { FormBase } from '@shared/models/form-base';
import { PaginatedList } from '@shared/models/paginated-list';
import { AlertService } from '@shared/modules/alert/alert.service';
import { ColumnMode } from '@swimlane/ngx-datatable';
import { map, startWith, takeUntil } from 'rxjs';

@Component({
  selector: 'majors-group',
  templateUrl: './majors-group.component.html',
  styleUrls: ['./majors-group.component.scss']
})
export class MajorsGroupComponent extends FormBase implements OnInit {
  columns = [
    { name: 'Id' },
    { name: 'Name' },
  ];

  rows: PaginatedList<MajorGroup>;
  pageSize: number = 10;
  pageNumber: number = 1;
  name = new FormControl('');
  ColumnMode = ColumnMode;

  constructor(private _majorGroupsService: MajorGroupService, private _alertService: AlertService) {
    super();
  }

  ngOnInit() {
    this.getMajorsGroupPagination();

    this.name.valueChanges.pipe(startWith(''), map(value => {
      this.pageNumber = 1;
      this.getMajorsGroupPagination();
    })).subscribe()
  }

  setPage(pageInfo) {
    this.pageNumber = pageInfo.offset;
    this.getMajorsGroupPagination();
  }

  getMajorsGroupPagination() {
    this._majorGroupsService.getMajorGroups(this.pageNumber, this.pageSize, this.name.value).subscribe(pagedData => {
      this.rows = pagedData
    });
  }

  deleteMajorGroup(id: number) {
    this._alertService.modalSize = "md";
    this._alertService.warn('Are you sure you want to delete this Major Groups !!!', this.rows.items.find(i => i.id == id).name, {
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
        this._majorGroupsService.deleteMajorGroups(id).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
          if (res) {
            this.getMajorsGroupPagination();
          }
        })
      }
    });
  }


}


