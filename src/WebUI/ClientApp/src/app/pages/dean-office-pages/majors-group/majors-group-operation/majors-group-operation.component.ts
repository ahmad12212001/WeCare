import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MajorGroupService } from '@app/pages/services/major-group.service';
import { FormBase } from '@shared/models/form-base';
import { takeUntil } from 'rxjs';

@Component({
  selector: 'majors-group-operation',
  templateUrl: './majors-group-operation.component.html',
  styleUrls: ['./majors-group-operation.component.scss']
})
export class MajorsGroupOperationComponent extends FormBase implements OnInit {

  majorGroupForm: FormGroup;

  constructor(private _fb: FormBuilder, private _majorGroupsService: MajorGroupService,
    private _route: ActivatedRoute, private _router: Router) {
    super();
  }

  ngOnInit() {
    this.createMajorGroupForm();
    const id = this._route.snapshot.params.id;
    if (id && id > 0) {
      this._majorGroupsService.getMajorGroup(id).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
        this.majorGroupForm.setValue({
          id: res.id,
          name: res.name
        });
      });
    }
  }

  createMajorGroupForm() {
    this.majorGroupForm = this._fb.group({
      name: new FormControl('', Validators.required),
      id: new FormControl(null)
    });
  }

  onSubmit() {

    if (this.majorGroupForm.invalid) {
      this.onSubmit$.next(null);
      setTimeout(() => {
        this.onSubmit$.next(true);
      }, 10);
      return;
    }

    if (this.majorGroupForm.value.id) {
      this._majorGroupsService.updateMajorGroup(this.majorGroupForm.value.id, this.majorGroupForm.value).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
        if (res && res.id) {
          this._router.navigate(['major-groups']);
        }
      })
    } else {
      this._majorGroupsService.createMajorGroup(this.majorGroupForm.value).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
        if (res && res > 0) {
          this._router.navigate(['major-groups']);
        }
      })
    }

  }

}
