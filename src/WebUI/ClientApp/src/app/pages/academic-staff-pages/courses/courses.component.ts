import { Component, OnInit } from '@angular/core';
import { ColumnMode } from '@swimlane/ngx-datatable';
import { CoursesService } from '@app/pages/services/courses.service';
import { PaginatedList } from '@shared/models/paginated-list';
import { Course } from '@app/pages/models/course';
import { FormControl } from '@angular/forms';
import { map, startWith, takeUntil } from 'rxjs';
import { AlertService } from '@shared/modules/alert/alert.service';
import { FormBase } from '@shared/models/form-base';

@Component({
  selector: 'app-course',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.scss']
})
export class CoursesComponent extends FormBase implements OnInit {
  columns = [
    { name: 'Id' },
    { name: 'Name' },
  ];

  rows: PaginatedList<Course>;
  pageSize: number = 10;
  pageNumber: number = 1;
  name = new FormControl('');
  ColumnMode = ColumnMode;

  constructor(private _courseService: CoursesService, private _alertService: AlertService) {
    super();
  }

  ngOnInit() {
    this.getCoursePagination();

    this.name.valueChanges.pipe(startWith(''), map(value => {
      this.pageNumber = 1;
      this.getCoursePagination();
    })).subscribe()
  }

  setPage(pageInfo) {
    this.pageNumber = pageInfo.offset;
    this.getCoursePagination();
  }

  getCoursePagination() {
    this._courseService.getCoursePagination(this.pageNumber, this.pageSize, this.name.value).subscribe(pagedData => {
      this.rows = pagedData
    });
  }

  deleteCourse(id: number) {
    this._alertService.modalSize = "md";
    this._alertService.warn('Are you sure you want to delete this Course !!!', this.rows.items.find(i => i.id == id).name, {
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
        this._courseService.delteteCourse(id).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
          if (res) {
            this.getCoursePagination();
          }
        })
      }
    });
  }


}


