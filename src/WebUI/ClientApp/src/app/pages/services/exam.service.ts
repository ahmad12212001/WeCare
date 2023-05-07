import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environment/environment';
import { Exam } from '../models/exam';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ExamService {

  private apiUrl: string = environment.apiUrl;

  constructor(private _http: HttpClient) { }


  getExam(id: number): Observable<Exam> {
    return this._http.get<Exam>(`${this.apiUrl}exams/${id}`);
  }

  createExam(exam: Exam) {
    return this._http.post(`${this.apiUrl}exams`, exam);
  }

  updateExam(exam: Exam) {
    return this._http.put(`${this.apiUrl}exams/${exam.id}`, exam);
  }

}
