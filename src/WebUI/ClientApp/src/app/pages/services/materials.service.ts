import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environment/environment';
import { Material } from '../models/material';
import { PaginatedList } from '@shared/models/paginated-list';
import { MaterialDto } from '../models/material-dto';

@Injectable({
  providedIn: 'root'
})
export class MaterialsService {

  private apiUrl: string = environment.apiUrl;

  constructor(private _http: HttpClient) { }

  createMaterial(material: Material) {
    let formData: FormData = new FormData();
    formData.append('file', material.file);
    formData.append('courseId', material.courseId.toString());
    formData.append('description', material.description);
    formData.append('name', material.name);
    if (material.requestId) {
      formData.append('requestId', material.requestId.toString());
    }

    return this._http.post(`${this.apiUrl}/materials`, formData, {
      reportProgress: true,
      observe: 'events'
    });
  }

  getMaterials(pageSize: number, pageNumber: number, requestId: number = null) {
    return this._http.get<PaginatedList<MaterialDto>>(`${this.apiUrl}materials`, {
      params: {
        pageNumber: pageNumber,
        pageSize: pageSize,
        requestId: requestId ?? ''
      }
    })
  }

}
