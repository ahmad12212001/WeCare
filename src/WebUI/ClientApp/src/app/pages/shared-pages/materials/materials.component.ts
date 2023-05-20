import { Component, OnInit } from '@angular/core';
import { MaterialDto } from '@app/pages/models/material-dto';
import { MaterialsService } from '@app/pages/services/materials.service';
import { FormBase } from '@shared/models/form-base';
import { PaginatedList } from '@shared/models/paginated-list';
import { takeUntil } from 'rxjs';

@Component({
  selector: 'app-materials',
  templateUrl: './materials.component.html',
  styleUrls: ['./materials.component.scss']
})
export class MaterialsComponent extends FormBase implements OnInit {
  materials: PaginatedList<MaterialDto>;
  pageSize: number = 10;
  pageNumber: number = 1;
  groupedMaterials: any;
  courseNames: string[];
  constructor(private _materialService: MaterialsService) {
    super();
  }

  ngOnInit() {
    this._materialService.getMaterials(this.pageSize, this.pageNumber).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
      this.materials = res;
      this.groupedMaterials = this.materials.items.reduce(function (results, org) {
        (results[org.courseName] = results[org.courseName] || []).push(org);
        return results;
      }, {});
      this.courseNames = Object.keys(this.groupedMaterials);

    })
  }

}
