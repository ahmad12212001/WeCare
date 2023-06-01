import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Request } from '@app/pages/models/request';
import { CoursesService } from '@app/pages/services/courses.service';
import { ExamsService } from '@app/pages/services/exams.service';
import { RequestsService } from '@app/pages/services/requests.service';
import { requiredWhenIsNull } from '@shared/custom-validatiors/required-when';
import { FormBase } from '@shared/models/form-base';
import { Option } from '@shared/models/option';
import { Observable, catchError, map, takeUntil } from 'rxjs';

@Component({
  selector: 'request-operation',
  templateUrl: './request-operation.component.html',
  styleUrls: ['./request-operation.component.scss']
})
export class RequestOperationComponent extends FormBase implements OnInit {

  requestForm: FormGroup;
  requestTypes: Option[] = [{ id: 2, name: "Material" }, { id: 4, name: "Assignment" }, { id: 1, name: "Exam" }];
  exams$: Observable<Option[]>;
  courses$: Observable<Option[]>;


  constructor(private _fb: FormBuilder,
    private _requestsService: RequestsService,
    private _examsService: ExamsService,
    private _courseService: CoursesService,
    private _router: Router) {
    super();
  }

  ngOnInit() {
    this.createCourseForm();

    this.exams$ = this._examsService.getExams().pipe(map(i => {
      return i.map(x => {
        return {
          id: x.id,
          name: x.name
        }
      })
    }));

    this.courses$ = this._courseService.getCourses().pipe(map(i => {
      return i.map(x => {
        return {
          id: x.id,
          name: x.name
        }
      })
    }));

  }

  createCourseForm() {
    this.requestForm = this._fb.group({
      dueDate: new FormControl('', Validators.required),
      requestType: new FormControl(null),
      exam: new FormControl(null),
      course: new FormControl(null, Validators.required),
      description: new FormControl('', Validators.required)
    },
      {
        validators: requiredWhenIsNull
      })

  }

  onSubmit() {
    if (this.requestForm.invalid) {
      this.onSubmit$.next(null);
      setTimeout(() => {
        this.onSubmit$.next(false);
      }, 10);
      return;
    }

    this.onSubmit$.next(true);
    let request: Request = {
      dueDate: this.requestForm.value.dueDate,
      requestType: this.requestForm.value.exam?.id ? null : this.requestForm.value.requestType.id,
      courseId: this.requestForm.value.course.id,
      examId: this.requestForm.value.exam?.id,
      description: this.requestForm.value.description
    };

    this._requestsService.createRequest(request).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
      if (res && res > 0) {
        this._router.navigate(['requests']);
      }
      this.onSubmit$.next(false);
    })


  }

}
