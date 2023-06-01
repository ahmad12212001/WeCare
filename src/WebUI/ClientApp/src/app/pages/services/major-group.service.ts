import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MajorGroup } from '../models/major-group';
import { environment } from '@environment/environment';
import { Observable } from 'rxjs';
import { PaginatedList } from '@shared/models/paginated-list';


@Injectable({
  providedIn: 'root'
})
export class MajorGroupService {
  apiUrl: string = environment.apiUrl;
  constructor(private _http: HttpClient) { }

  createMajorGroup(majorGroup: MajorGroup): Observable<number> {
    return this._http.post<number>(`${this.apiUrl}majorgroups`, majorGroup);
  }

  updateMajorGroup(id: number, majorGroup: MajorGroup): Observable<MajorGroup> {
    return this._http.put<MajorGroup>(`${this.apiUrl}majorgroups/${id}`, majorGroup);
  }

  getMajorGroup(id: number): Observable<MajorGroup> {
    return this._http.get<MajorGroup>(`${this.apiUrl}majorgroups/${id}`);
  }

  getMajorGroups(pageNumber: number, pageSize: number, name: string = ''): Observable<PaginatedList<MajorGroup>> {
    return this._http.get<PaginatedList<MajorGroup>>(`${this.apiUrl}majorgroups`, {
      params: {
        pageNumber: pageNumber,
        pageSize: pageSize,
        name: name
      }
    });
  }

  getMajorGroupsList(): Observable<MajorGroup[]> {
    return this._http.get<MajorGroup[]>(`${this.apiUrl}majorgroups/list`);
  }

  deleteMajorGroups(id: number): Observable<boolean> {
    return this._http.delete<boolean>(`${this.apiUrl}majorgroups/${id}`);
  }
}
