

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Course } from '@app/pages/models/course';
import { CoursesService } from '@app/pages/services/courses.service';
import { MajorGroupService } from '@app/pages/services/major-group.service';
import { UsersService } from '@app/pages/services/users.service';
import { FormBase } from '@shared/models/form-base'
import { Option } from '@shared/models/option';
import { map, takeUntil } from 'rxjs';

@Component({
  selector: 'course-operation',
  templateUrl: './course-operation.component.html',
  styleUrls: ['./course-operation.component.scss']
})
export class CourseOperationComponent extends FormBase implements OnInit {

  courseForm: FormGroup;
  academicStaff: Option[] = [];
  majorGroups: Option[];
  constructor(private _fb: FormBuilder,
    private _courseService: CoursesService,
    private _userService: UsersService,
    private route: ActivatedRoute, private _majorGroupsService: MajorGroupService, private _router: Router) {
    super();
  }


  ngOnInit() {
    this.createCourseForm();
    this._majorGroupsService.getMajorGroupsList().pipe(takeUntil(this.onDestroy$), map(i => {
      return i.map(m => {
        return {
          id: m.id,
          name: m.name
        }
      })
    })).subscribe(majorGroups => {
      this.majorGroups = majorGroups;
      this._userService.getUsers('AcademicStaff').pipe(takeUntil(this.onDestroy$), map(i => {
        return i.map(u => {
          return {
            id: u.id,
            name: `${u.firstName} ${u.lastName}`
          }
        })
      })).subscribe(response => {
        this.academicStaff = response;
        const id = this.route.snapshot.params.id;
        if (id && id > 0) {
          this._courseService.getCourse(id).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
            this.courseForm.setValue({
              id: res.id,
              name: res.name,
              user: this.academicStaff.find(i => i.id == res.userId),
              majorGroup: this.majorGroups.find(i => i.id == res.majorGroupId)
            })
          })
        }
      })
    })




  }

  createCourseForm() {
    this.courseForm = this._fb.group({
      name: new FormControl('', Validators.required),
      id: new FormControl(null),
      user: new FormControl(null, Validators.required),
      majorGroup: new FormControl(null, Validators.required)
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

    let course: Course = {
      id: this.courseForm.value.id,
      name: this.courseForm.value.name,
      userId: this.courseForm.value.user.id,
      majorGroupId: this.courseForm.value.majorGroup.id
    }

    if (this.courseForm.value.id) {
      this._courseService.updateCourse(this.courseForm.value.id, course).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
        if (res && res.id) {
          this._router.navigate(['courses']);
        }

      })
    } else {
      this._courseService.createCourse(course).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
        if (res && res > 0) {
          this._router.navigate(['courses']);
        }

      })
    }

  }

}
