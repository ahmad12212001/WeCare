import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Student } from '@app/pages/models/student';
import { StudentService } from '@app/pages/services/student.service';
import { FormBase } from '@shared/models/form-base';
import { PaginatedList } from '@shared/models/paginated-list';
import { AlertService } from '@shared/modules/alert/alert.service';
import { ColumnMode } from '@swimlane/ngx-datatable';
import { map, startWith, takeUntil } from 'rxjs';

@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.scss']
})
export class StudentsComponent extends FormBase implements OnInit {
  rows: PaginatedList<Student>;
  pageSize: number = 10;
  pageNumber: number = 1;
  name = new FormControl('');
  ColumnMode = ColumnMode;

  constructor(private _studentService: StudentService, private _alertService: AlertService) {
    super();
  }

  ngOnInit() {
    this.getStudentsPagination();

    this.name.valueChanges.pipe(startWith(''), map(value => {
      this.pageNumber = 1;
      this.getStudentsPagination();
    })).subscribe()
  }

  setPage(pageInfo) {
    this.pageNumber = pageInfo.offset;
    this.getStudentsPagination();
  }

  getStudentsPagination() {
    this._studentService.getStudents(this.pageSize, this.pageNumber, this.name.value).subscribe(pagedData => {
      this.rows = pagedData
    });
  }

  deleteStudent(id: number) {
    this._alertService.modalSize = "md";
    this._alertService.warn('Are you sure you want to delete this Student !!!', this.rows.items.find(i => i.id == id).studentId, {
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
        this._studentService.delteteStudent(id).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
          if (res) {
            this.getStudentsPagination();
          }
        })
      }
    });
  }


}


