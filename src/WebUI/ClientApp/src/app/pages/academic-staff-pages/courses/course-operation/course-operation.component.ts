

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CoursesService } from '@app/pages/services/courses.service';
import { FormBase } from '@shared/models/form-base'
import { Option } from '@shared/models/option';
import { takeUntil } from 'rxjs';

@Component({
  selector: 'course-operation',
  templateUrl: './course-operation.component.html',
  styleUrls: ['./course-operation.component.scss']
})
export class CourseOperationComponent extends FormBase implements OnInit {

  courseForm: FormGroup;
  courses: Option[] = [];

  constructor(private _fb: FormBuilder,

    private _courseService: CoursesService,
    private route: ActivatedRoute) {
    super();
  }

  ngOnInit() {
    this.createCourseForm();
    const id = this.route.snapshot.params.id;
    if (id && id > 0) {
      this._courseService.getCourse(id).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
        this.courseForm.setValue({
          id: res.id,
          name: res.name
        })
      })
    }



  }

  createCourseForm() {
    this.courseForm = this._fb.group({
      name: new FormControl('', Validators.required),
      id: new FormControl(null)
    })
  }

  onSubmit() {

    if (this.courseForm.invalid) {
      this.onSubmit$.next(null);
      setTimeout(() => {
        this.onSubmit$.next(true);
      }, 10);
      return;
    }

    if (this.courseForm.value.id) {
      this._courseService.updateCourse(this.courseForm.value.id, this.courseForm.value).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
        debugger
      })
    } else {
      this._courseService.createCourse(this.courseForm.value).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
        debugger
      })
    }

  }

}
