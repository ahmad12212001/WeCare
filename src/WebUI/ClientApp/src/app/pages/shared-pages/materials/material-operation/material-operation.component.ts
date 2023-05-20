import { HttpEventType } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Material } from '@app/pages/models/material';
import { CoursesService } from '@app/pages/services/courses.service';
import { MaterialsService } from '@app/pages/services/materials.service';
import { ActionControl } from '@shared/models/action-control';
import { FormBase } from '@shared/models/form-base';
import { Option } from '@shared/models/option';
import { FileUploadOptions } from '@shared/modules/file-upload/file-upload-options';
import { Observable, map, takeUntil } from 'rxjs';


@Component({
  selector: 'material-operation',
  templateUrl: './material-operation.component.html',
  styleUrls: ['./material-operation.component.scss']
})
export class MaterialOperationComponent extends FormBase implements OnInit {

  materialForm: FormGroup;
  requestId: string;
  user: any;
  courses$: Observable<Option[]>;
  options: FileUploadOptions = {
    isRequired: true,
    isMulti: false,
    error: "Please Select Valid File",
    progress$: new ActionControl()
  };

  constructor(private _fb: FormBuilder, private _route: ActivatedRoute,
    private _materialService: MaterialsService,
    private _courseService: CoursesService) {
    super();
  }

  ngOnInit() {
    this.requestId = this._route.snapshot.params.requestId ?? '';
    this.courses$ = this._courseService.getCourses().pipe(map(c => c.map(course => {
      return {
        id: course.id,
        name: course.name
      }
    })));
    this.createMaterialForm();

  }

  createMaterialForm() {
    this.materialForm = this._fb.group({
      course: new FormControl(null, Validators.required),
      file: new FormControl(null, Validators.required),
      name: new FormControl(null, Validators.required),
      description: new FormControl(null, Validators.required),
      requestId: new FormControl(this.requestId)
    });
  }

  onSubmit() {
    if (this.materialForm.invalid) {
      this.onSubmit$.next(null);
      setTimeout(() => {
        this.onSubmit$.next(true);
      }, 10);
      return;
    }

    let material: Material = {
      courseId: this.materialForm.value.course.id,
      name: this.materialForm.value.name,
      description: this.materialForm.value.description,
      file: this.materialForm.value.file,
      requestId: this.materialForm.value.requestId
    };

    this._materialService.createMaterial(material).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
      if (res.type === HttpEventType.UploadProgress) {
        this.options.progress$.value = (Math.round(100 * res.loaded / res.total));
      }
    })
  }

}
