import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Major } from '@app/pages/models/major';
import { MajorGroupService } from '@app/pages/services/major-group.service';
import { MajorService } from '@app/pages/services/major.service';
import { FormBase } from '@shared/models/form-base';
import { Option } from '@shared/models/option';
import { Observable, map, takeUntil } from 'rxjs';

@Component({
  selector: 'app-majors-operation',
  templateUrl: './majors-operation.component.html',
  styleUrls: ['./majors-operation.component.scss']
})
export class MajorsOperationComponent extends FormBase implements OnInit {

  majorForm: FormGroup;
  majorGroups: Option[];
  constructor(private _fb: FormBuilder, private _majorService: MajorService,
    private _route: ActivatedRoute, private _majorGroupsService: MajorGroupService, private _router: Router) {
    super();
  }

  ngOnInit() {
    this.createMajorGroupForm();
    const id = this._route.snapshot.params.id;
    this._majorGroupsService.getMajorGroupsList().pipe(takeUntil(this.onDestroy$), map(i => {
      return i.map(m => {
        return {
          id: m.id,
          name: m.name
        }
      })
    })).subscribe(res => {
      this.majorGroups = res;

      if (id && id > 0) {
        this._majorService.getMajor(id).pipe(takeUntil(this.onDestroy$)).subscribe(response => {
          debugger
          this.majorForm.setValue({
            id: response.id,
            name: response.name,
            majorGroup: this.majorGroups.find(i => i.id == response.majorGroupId)
          });
        });
      }

    })


  }

  createMajorGroupForm() {
    this.majorForm = this._fb.group({
      name: new FormControl('', Validators.required),
      id: new FormControl(null),
      majorGroup: new FormControl(null, Validators.required)
    });
  }

  onSubmit() {
    if (this.majorForm.invalid) {
      this.onSubmit$.next(null);
      setTimeout(() => {
        this.onSubmit$.next(true);
      }, 10);
      return;
    }

    let major: Major = {
      id: this.majorForm.value.id,
      name: this.majorForm.value.name,
      majorGroupId: this.majorForm.value.majorGroup.id,
      majorGroupName: this.majorForm.value.majorGroup.name
    }

    if (this.majorForm.value.id) {
      this._majorService.updateMajor(this.majorForm.value.id, major).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
        if (res.id) {
          this._router.navigate(['majors']);
        }

      })
    } else {
      this._majorService.createMajor(major).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
        if (res && res > 0) {
          this._router.navigate(['majors']);
        }

      })
    }

  }

}
