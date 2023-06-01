import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environment/environment';
import { Major } from '../models/major';
import { Observable } from 'rxjs';
import { PaginatedList } from '@shared/models/paginated-list';

@Injectable({
  providedIn: 'root'
})
export class MajorService {
  apiUrl: string = environment.apiUrl;

  constructor(private _http: HttpClient) { }

  createMajor(majorGroup: Major): Observable<number> {
    return this._http.post<number>(`${this.apiUrl}majors`, majorGroup);
  }

  updateMajor(id: number, majorGroup: Major): Observable<Major> {
    return this._http.put<Major>(`${this.apiUrl}majors/${id}`, majorGroup);
  }

  getMajor(id: number): Observable<Major> {
    return this._http.get<Major>(`${this.apiUrl}majors/${id}`);
  }

  getMajors(pageNumber: number, pageSize: number, name: string = ''): Observable<PaginatedList<Major>> {
    return this._http.get<PaginatedList<Major>>(`${this.apiUrl}majors`, {
      params: {
        pageNumber: pageNumber,
        pageSize: pageSize,
        name: name
      }
    });
  }

  getMajorsList(): Observable<Major[]> {
    return this._http.get<Major[]>(`${this.apiUrl}majors/list`);
  }

  deleteMajor(id: number): Observable<boolean> {
    return this._http.delete<boolean>(`${this.apiUrl}majors/${id}`);
  }
}
