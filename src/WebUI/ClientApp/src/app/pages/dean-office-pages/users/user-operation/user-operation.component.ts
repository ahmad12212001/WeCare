import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '@app/pages/models/user';
import { MajorService } from '@app/pages/services/major.service';
import { UsersService } from '@app/pages/services/users.service';
import { FormBase } from '@shared/models/form-base';
import { Option } from '@shared/models/option';
import { map, takeUntil } from 'rxjs';

@Component({
  selector: 'app-user-operation',
  templateUrl: './user-operation.component.html',
  styleUrls: ['./user-operation.component.scss']
})
export class UserOperationComponent extends FormBase implements OnInit {
  userForm: FormGroup;
  majors: Option[] = [];
  roles: Option[] = [
    {
      id: 1,
      name: 'AcademicStaff'
    },
    {
      id: 2,
      name: 'VolunteerStudent'
    }, {
      id: 3,
      name: 'DisabilityStudent'
    }, {
      id: 4,
      name: 'DeanOffice'
    }
  ]
  constructor(private _fb: FormBuilder,
    private _majorService: MajorService, private _route: ActivatedRoute,
    private _usersService: UsersService, private _router: Router) {
    super();
  }

  ngOnInit() {
    this.createUserForm();
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
      if (id != null || id != '') {
        this._usersService.getUser(id).pipe(takeUntil(this.onDestroy$)).subscribe(response => {
          this.userForm.setValue({
            studentId: response.studentId,
            major: this.majors.find(i => i.name == response.major),
            firstName: response.firstName,
            lastName: response.lastName,
            email: response.email,
            role: this.roles.find(i => i.name == response.role),
            id: response.id,
            phoneNumber: response.phoneNumber
          });
        });
      }
    })
  }

  createUserForm() {
    this.userForm = this._fb.group({
      studentId: new FormControl(null),
      major: new FormControl(null),
      firstName: new FormControl(null, Validators.required),
      lastName: new FormControl(null, Validators.required),
      email: new FormControl(null, Validators.required),
      role: new FormControl(null, Validators.required),
      phoneNumber: new FormControl(null, Validators.required),
      id: new FormControl(null)
    })
  }


  onSubmit() {
    if (this.userForm.invalid) {
      this.onSubmit$.next(null);
      setTimeout(() => {
        this.onSubmit$.next(true);
      }, 10);
      return;
    }

    let user: User = {
      id: this.userForm.value.id,
      firstName: this.userForm.value.firstName,
      major: this.userForm?.value?.major?.name,
      role: this.userForm.value?.role?.name,
      email: this.userForm.value.email,
      phoneNumber: this.userForm.value.phoneNumber,
      lastName: this.userForm.value.lastName,
      studentId: this.userForm.value?.studentId
    }

    if (this.userForm.value.id) {
      this._usersService.updateUser(user).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
        if (res && res.id) {
          this._router.navigate(['users']);
        }
      })
    } else {
      this._usersService.createUser(user).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
        if (res && (res != null || res != '')) {
          this._router.navigate(['users']);
        }
      })
    }

  }


}
