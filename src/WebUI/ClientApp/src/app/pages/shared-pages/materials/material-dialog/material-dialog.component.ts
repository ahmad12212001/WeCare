import { Component, OnInit } from '@angular/core';
import { MaterialDto } from '@app/pages/models/material-dto';
import { MaterialsService } from '@app/pages/services/materials.service';
import { FormBase } from '@shared/models/form-base';
import { PaginatedList } from '@shared/models/paginated-list';
import { takeUntil } from 'rxjs';

@Component({
  selector: 'app-material-dialog',
  templateUrl: './material-dialog.component.html',
  styleUrls: ['./material-dialog.component.scss']
})
export class MaterialDialogComponent extends FormBase implements OnInit {

  materials: PaginatedList<MaterialDto>;
  pageSize: number = 10;
  pageNumber: number = 1;
  requestId: number;

  constructor(private _materialService: MaterialsService) {
    super();
  }

  ngOnInit() {
    this._materialService.getMaterials(this.pageSize, this.pageNumber, this.requestId).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
      this.materials = res;
    });
  }

  close() {
    this.closeDialog$.next(true);
  }

}
