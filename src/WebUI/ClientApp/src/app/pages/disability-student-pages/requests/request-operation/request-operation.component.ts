import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Request } from '@app/pages/models/request';
import { CoursesService } from '@app/pages/services/courses.service';
import { ExamService } from '@app/pages/services/exam.service';
import { RequestService } from '@app/pages/services/request.service';
import { requiredWhenIsNull } from '@shared/custom-validatiors/required-when';
import { FormBase } from '@shared/models/form-base';
import { Option } from '@shared/models/option';
import { Observable, map, takeUntil } from 'rxjs';

@Component({
  selector: 'request-operation',
  templateUrl: './request-operation.component.html',
  styleUrls: ['./request-operation.component.scss']
})
export class RequestOperationComponent extends FormBase implements OnInit {

  requestForm: FormGroup;
  requestTypes: Option[] = [{ id: 2, name: "Material" }, { id: 4, name: "Assignment" }];
  exams$: Observable<Option[]>;
  courses$: Observable<Option[]>;


  constructor(private _fb: FormBuilder,
    private _requestService: RequestService,
    private _examService: ExamService,
    private _courseService: CoursesService) {
    super();
  }

  ngOnInit() {
    this.createCourseForm();

    this.exams$ = this._examService.getExams().pipe(map(i => {
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
        this.onSubmit$.next(true);
      }, 10);
      return;
    }

    let request: Request = {
      dueDate: this.requestForm.value.dueDate,
      requestType: this.requestForm.value.exam?.id ? null : this.requestForm.value.requestType.id,
      courseId: this.requestForm.value.course.id,
      examId: this.requestForm.value.exam?.id,
      description: this.requestForm.value.description
    };

    this._requestService.createRequest(request).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
      debugger
    })


  }

}
