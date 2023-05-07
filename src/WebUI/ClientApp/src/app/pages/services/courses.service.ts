import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environment/environment';
import { Observable } from 'rxjs';
import { Course } from '../models/course';

@Injectable({
  providedIn: 'root'
})
export class CoursesService {
  private apiUrl: string = environment.apiUrl;

  constructor(private _http: HttpClient) { }

  getCourses(): Observable<Course[]> {
    return this._http.get<Course[]>(`${this.apiUrl}courses`);
  }

  getAcacdemicStaffCourses(): Observable<Course[]> {
    return this._http.get<Course[]>(`${this.apiUrl}courses/academic`);
  }

}
