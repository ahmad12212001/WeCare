import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Course } from '@app/pages/models/course';
import { StudetCourse } from '@app/pages/models/student-course';
import { CoursesService } from '@app/pages/services/courses.service';
import { StudentCoursesService } from '@app/pages/services/student-courses.service';
import { FormBase } from '@shared/models/form-base';
import { Observable, takeUntil } from 'rxjs';

@Component({
  selector: 'student-course-operation',
  templateUrl: './student-course-operation.component.html',
  styleUrls: ['./student-course-operation.component.scss']
})
export class StudentCourseOperationComponent extends FormBase implements OnInit {

  studentCoursesForm: FormGroup;
  studentCourses: Course[];
  courses$: Observable<Course[]>;
  dropdownSettings = {
    singleSelection: false,
    idField: 'id',
    textField: 'name',
    selectAllText: 'Select All',
    unSelectAllText: 'UnSelect All',
  }
  studentId: number;

  constructor(private _studentCoursesService: StudentCoursesService,
    private _fb: FormBuilder, private _coursesService: CoursesService, private _route: ActivatedRoute,
    private _router: Router) {
    super()
  }

  ngOnInit() {
    this.createStudentCoursesForm();
    this.studentId = this._route.snapshot.params.id;
    this.courses$ = this._coursesService.getCourses();
    this._studentCoursesService.getStudentAvailableCourse(this.studentId).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
      this.studentCourses = res;
      this.studentCoursesForm.setValue({
        studentId: this.studentId,
        courses: this.studentCourses
      })

    })

  }

  createStudentCoursesForm() {
    this.studentCoursesForm = this._fb.group({
      studentId: new FormControl(null, Validators.required),
      courses: new FormControl(null, Validators.required)
    })
  }

  onSubmit() {
    if (this.studentCoursesForm.invalid) {
      this.onSubmit$.next(null);
      setTimeout(() => {
        this.onSubmit$.next(true);
      }, 10);
      return;
    }

    let studentCourses: StudetCourse = {
      studentId: this.studentCoursesForm.value.studentId,
      courses: this.studentCoursesForm.value.courses.map(i => i.id)
    };

    this._studentCoursesService.studentCourse(studentCourses).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
      if (res) {
        this._router.navigate(['students']);
      }
    })
  }
}
