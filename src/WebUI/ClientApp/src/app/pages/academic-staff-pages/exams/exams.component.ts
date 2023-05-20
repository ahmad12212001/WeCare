import { Component, OnInit } from '@angular/core';
import { ColumnMode } from '@swimlane/ngx-datatable';
import { ExamsService } from '@app/pages/services/exams.service';
import { PaginatedList } from '@shared/models/paginated-list';
import { Exam } from '@app/pages/models/exam';
import { FormControl } from '@angular/forms';
import { map, startWith, takeUntil } from 'rxjs';
import { AlertService } from '@shared/modules/alert/alert.service';
import { FormBase } from '@shared/models/form-base';

@Component({
  selector: 'app-exams',
  templateUrl: './exams.component.html',
  styleUrls: ['./exams.component.scss']
})
export class ExamsComponent extends FormBase implements OnInit {
  columns = [
    { name: 'Id' },
    { name: 'Name' },
    { name: 'DueDate' },
    { name: 'Location' },
    { name: 'HallNo' }
  ];

  rows: PaginatedList<Exam>;
  pageSize: number = 10;
  pageNumber: number = 1;
  name = new FormControl('');
  ColumnMode = ColumnMode;

  constructor(private _ExamsService: ExamsService, private _alertService: AlertService) {
    super();
  }

  ngOnInit() {
    this.getExamPagination();

    this.name.valueChanges.pipe(startWith(''), map(value => {
      this.pageNumber = 1;
      this.getExamPagination();
    })).subscribe()
  }

  setPage(pageInfo) {
    this.pageNumber = pageInfo.offset;
    this.getExamPagination();
  }

  getExamPagination() {
    this._ExamsService.getExamsPagination(this.pageNumber, this.pageSize, this.name.value).subscribe(pagedData => {
      this.rows = pagedData
    });
  }

  deleteExam(id: number) {
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
        this._ExamsService.deleteExam(id).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
          if (res) {
            this.getExamPagination();
          }
        })
      }
    });
  }
  
}

