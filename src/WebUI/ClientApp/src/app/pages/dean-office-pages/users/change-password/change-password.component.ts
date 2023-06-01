import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { UserChangePassword } from '@app/pages/models/user-change-password';
import { UsersService } from '@app/pages/services/users.service';
import { FormBase } from '@shared/models/form-base';
import { takeUntil } from 'rxjs';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent extends FormBase implements OnInit {
  userForm: FormGroup;
  userId: string;
  notMatched: boolean;
  constructor(private _fb: FormBuilder,
    private _route: ActivatedRoute, private _usersService: UsersService) {
    super();
  }

  ngOnInit() {
    this.userId = this._route.snapshot.params.id;

    this.createUserForm();

  }

  createUserForm() {
    this.userForm = this._fb.group({
      password: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(6),
        Validators.maxLength(30)
      ])),
      confirmpassword: new FormControl('', Validators.compose([
        Validators.required,
        Validators.minLength(6),
        Validators.maxLength(30)
      ])),
    }, {
      validators: this.password
    });

  }

  get f() {
    return this.userForm.controls;
  }

  password(formGroup: FormGroup) {
    const { value: password } = formGroup.get('password');
    const { value: confirmPassword } = formGroup.get('confirmpassword');
    return password === confirmPassword ? null : { passwordNotMatch: true };
  }

  onSubmit() {
    if (this.userForm.invalid) {
      if (this.userForm.errors) {
        if (this.userForm.errors.passwordNotMatch) {
          this.notMatched = true;
        }
      }

      this.onSubmit$.next(null);
      setTimeout(() => {
        this.onSubmit$.next(true);
      }, 10);
      return;
    }

    let user: UserChangePassword = {
      id: this.userId,
      password: this.userForm.value.password
    }


    this._usersService.updateUserPassword(user).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
      debugger
    })


  }


}
