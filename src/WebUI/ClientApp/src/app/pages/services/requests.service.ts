import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environment/environment';
import { Request } from '../models/request';
import { Observable } from 'rxjs';
import { PaginatedList } from '../../shared/models/paginated-list';
import { RequestDto } from '../models/request-dtos';
import { RequestFeedback } from '../models/request-feedback';
import { Review } from '@shared/models/review';

@Injectable({
  providedIn: 'root'
})
export class RequestsService {

  private apiUrl: string = environment.apiUrl;

  constructor(private _http: HttpClient) { }

  createRequest(request: Request) {
    return this._http.post(`${this.apiUrl}requests`, request);
  }

  getRequest(id: number) {
    return this._http.get(`${this.apiUrl}requests/${id}`);
  }

  deleteRequest(id: number) {
    return this._http.delete(`${this.apiUrl}requests/${id}`);
  }

  getRequestsPagination(pageNumber: number, pageSize: number, name: string = ''): Observable<PaginatedList<RequestDto>> {
    return this._http.get<PaginatedList<RequestDto>>(`${this.apiUrl}requests`, {
      params: {
        pageNumber: pageNumber,
        pageSize: pageSize,
        description: name
      }
    });
  }

  acceptRequest(request: RequestDto) {
    return this._http.post<number>(`${this.apiUrl}requestVolunteers`, { requestId: request.id });
  }

  addRequestFeedback(requestFeedback: RequestFeedback) {
    return this._http.post<number>(`${this.apiUrl}requestfeedbacks`, requestFeedback);
  }

  getRequestFeedbacks(request: RequestDto) {
    return this._http.get<Review[]>(`${this.apiUrl}requestfeedbacks`, {
      params: {
        requestId: request.id
      }
    });
  }

}
