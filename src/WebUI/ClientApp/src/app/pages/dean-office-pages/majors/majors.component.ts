import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Major } from '@app/pages/models/major';
import { MajorService } from '@app/pages/services/major.service';
import { FormBase } from '@shared/models/form-base';
import { PaginatedList } from '@shared/models/paginated-list';
import { AlertService } from '@shared/modules/alert/alert.service';
import { ColumnMode } from '@swimlane/ngx-datatable';
import { map, startWith, takeUntil } from 'rxjs';

@Component({
  selector: 'majors',
  templateUrl: './majors.component.html',
  styleUrls: ['./majors.component.scss']
})
export class MajorsComponent extends FormBase implements OnInit {
  columns = [
    { name: 'Id' },
    { name: 'Name' },
  ];

  rows: PaginatedList<Major>;
  pageSize: number = 10;
  pageNumber: number = 1;
  name = new FormControl('');
  ColumnMode = ColumnMode;

  constructor(private _majorService: MajorService, private _alertService: AlertService) {
    super();
  }

  ngOnInit() {
    this.getMajorsPagination();

    this.name.valueChanges.pipe(startWith(''), map(value => {
      this.pageNumber = 1;
      this.getMajorsPagination();
    })).subscribe()
  }

  setPage(pageInfo) {
    this.pageNumber = pageInfo.offset;
    this.getMajorsPagination();
  }

  getMajorsPagination() {
    this._majorService.getMajors(this.pageNumber, this.pageSize, this.name.value).subscribe(pagedData => {
      this.rows = pagedData
    });
  }

  deleteMajor(id: number) {
    this._alertService.modalSize = "md";
    this._alertService.warn('Are you sure you want to delete this Major !!!', this.rows.items.find(i => i.id == id).name, {
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
        this._majorService.deleteMajor(id).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
          if (res) {
            this.getMajorsPagination();
          }
        })
      }
    });
  }


}


