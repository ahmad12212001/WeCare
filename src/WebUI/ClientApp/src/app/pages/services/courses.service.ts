import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environment/environment';
import { Observable } from 'rxjs';
import { Course } from '../models/course';
import { PaginatedList } from '../../shared/models/paginated-list';


@Injectable({
  providedIn: 'root'
})
export class CoursesService {
  private apiUrl: string = environment.apiUrl;

  constructor(private _http: HttpClient) { }

  getCourses(): Observable<Course[]> {
    return this._http.get<Course[]>(`${this.apiUrl}courses/list`);
  }

  getAcacdemicStaffCourses(): Observable<Course[]> {
    return this._http.get<Course[]>(`${this.apiUrl}courses/academic`);
  }

  getCoursePagination(pageNumber: number, pageSize: number, name: string = ''): Observable<PaginatedList<Course>> {
    return this._http.get<PaginatedList<Course>>(`${this.apiUrl}courses`, {
      params: {
        pageNumber: pageNumber,
        pageSize: pageSize,
        name: name
      }
    });
  }

  createCourse(course: Course): Observable<number> {
    return this._http.post<number>(`${this.apiUrl}courses`, course);
  }

  updateCourse(id: number, course: Course): Observable<Course> {
    return this._http.put<Course>(`${this.apiUrl}courses/${id}`, course);
  }

  getCourse(id: number): Observable<Course> {
    return this._http.get<Course>(`${this.apiUrl}courses/${id}`);
  }

  delteteCourse(id: number): Observable<string> {
    return this._http.delete<string>(`${this.apiUrl}courses/${id}`);
  }

}
