import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Student } from '@app/pages/models/student';
import { MajorService } from '@app/pages/services/major.service';
import { StudentService } from '@app/pages/services/student.service';
import { FormBase } from '@shared/models/form-base';
import { Option } from '@shared/models/option';
import { map, takeUntil } from 'rxjs';

@Component({
  selector: 'student-operation',
  templateUrl: './student-operation.component.html',
  styleUrls: ['./student-operation.component.scss']
})
export class StudentOperationComponent extends FormBase implements OnInit {
  studentForm: FormGroup;
  majors: Option[] = [];
  studentTypes: Option[] = [
    {
      id: 0,
      name: 'DisabilityStudent'
    },
    {
      id: 1,
      name: 'VolunteerStudent'
    }
  ]
  constructor(private _fb: FormBuilder,
    private _majorService: MajorService, private _route: ActivatedRoute, private _studentService: StudentService,
    private _router: Router) {
    super();
  }

  ngOnInit() {
    this.createStudentForm();
    const id = this._route.snapshot.params.id;
    this._majorService.getMajorsList().pipe(takeUntil(this.onDestroy$), map(i => {
      return i.map(u => {
        return {
          id: u.id,
          name: u.name
        }
      })
    })).subscribe(res => {
      this.majors = res;
      if (id && id > 0) {
        this._studentService.getStudent(id).pipe(takeUntil(this.onDestroy$)).subscribe(response => {
          this.studentForm.setValue({
            studentId: response.studentId,
            major: this.majors.find(i => i.name == response.major),
            firstName: response.firstName,
            lastName: response.lastName,
            email: response.email,
            type: this.studentTypes.find(i => i.name == response.type),
            id: response.id,
            phoneNumber: response.phoneNumber
          });
        });
      }
    })
  }

  createStudentForm() {
    this.studentForm = this._fb.group({
      studentId: new FormControl(null, Validators.required),
      major: new FormControl(null, Validators.required),
      firstName: new FormControl(null, Validators.required),
      lastName: new FormControl(null, Validators.required),
      email: new FormControl(null, Validators.required),
      type: new FormControl(null, Validators.required),
      phoneNumber: new FormControl(null, Validators.required),
      id: new FormControl(null)
    })
  }


  onSubmit() {
    if (this.studentForm.invalid) {
      this.onSubmit$.next(null);
      setTimeout(() => {
        this.onSubmit$.next(true);
      }, 10);
      return;
    }

    let student: Student = {
      id: this.studentForm.value.id,
      firstName: this.studentForm.value.firstName,
      major: this.studentForm.value.major.name,
      type: this.studentForm.value.type.id,
      email: this.studentForm.value.email,
      phoneNumber: this.studentForm.value.phoneNumber,
      lastName: this.studentForm.value.lastName,
      studentId: this.studentForm.value.studentId
    }

    if (this.studentForm.value.id) {
      this._studentService.updateStudent(student).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
        if (res && res.id) {
          this._router.navigate(['students']);
        }

      })
    } else {
      this._studentService.createStudent(student).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
        if (res && res > 0) {
          this._router.navigate(['students']);
        }

      })
    }

  }


}
