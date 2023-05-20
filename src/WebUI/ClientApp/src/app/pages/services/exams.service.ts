import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environment/environment';
import { Exam } from '../models/exam';
import { Observable } from 'rxjs';
import { PaginatedList } from '../../shared/models/paginated-list';

@Injectable({
  providedIn: 'root'
})
export class ExamsService {

  private apiUrl: string = environment.apiUrl;

  constructor(private _http: HttpClient) { }


  getExam(id: number): Observable<Exam> {
    return this._http.get<Exam>(`${this.apiUrl}exams/${id}`);
  }

  getExams(): Observable<Exam[]> {
    return this._http.get<Exam[]>(`${this.apiUrl}exams/list`);
  }

  getExamsPagination(pageNumber: number, pageSize: number, name: string = ''): Observable<PaginatedList<Exam>> {
    return this._http.get<PaginatedList<Exam>>(`${this.apiUrl}exams`, {
      params: {
        pageNumber: pageNumber,
        pageSize: pageSize,
        name: name
      }
    });
  }

  createExam(exam: Exam) {
    return this._http.post(`${this.apiUrl}exams`, exam);
  }

  updateExam(exam: Exam) {
    return this._http.put(`${this.apiUrl}exams/${exam.id}`, exam);
  }

  deleteExam(id: number) {
    return this._http.delete<Exam>(`${this.apiUrl}exams/${id}`);
  }

}
