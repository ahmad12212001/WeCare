import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environment/environment';
import { StudetCourse } from '../models/student-course';
import { Observable } from 'rxjs';
import { Course } from '../models/course';

@Injectable({
  providedIn: 'root'
})
export class StudentCoursesService {

  apiUrl: string = environment.apiUrl;

  constructor(private _http: HttpClient) { }

  studentCourse(studentCourse: StudetCourse): Observable<any> {
    return this._http.post<any>(`${this.apiUrl}studentcourses`, studentCourse);
  }

  getStudentAvailableCourse(studentId: number): Observable<Course[]> {
    return this._http.get<Course[]>(`${this.apiUrl}studentcourses/${studentId}`);
  }
}
