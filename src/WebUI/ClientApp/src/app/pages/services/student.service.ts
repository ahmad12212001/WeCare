import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environment/environment';
import { Student } from '../models/student';
import { Observable } from 'rxjs';
import { PaginatedList } from '@shared/models/paginated-list';

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  apiUrl: string = environment.apiUrl;

  constructor(private _http: HttpClient) { }

  createStudent(student: Student): Observable<number> {
    return this._http.post<number>(`${this.apiUrl}students`, student);
  }

  updateStudent(student: Student): Observable<Student> {
    return this._http.put<Student>(`${this.apiUrl}students`, student);
  }

  getStudent(id: number): Observable<Student> {
    return this._http.get<Student>(`${this.apiUrl}students/${id}`);
  }

  getStudents(pageSize: number, pageNumber: number, name: string = '') {
    return this._http.get<PaginatedList<Student>>(`${this.apiUrl}students`, {
      params: {
        pageNumber: pageNumber,
        pageSize: pageSize,
        name: name
      }
    })
  }

  delteteStudent(id: number) {
    return this._http.delete<Student>(`${this.apiUrl}students/${id}`);
  }
}
