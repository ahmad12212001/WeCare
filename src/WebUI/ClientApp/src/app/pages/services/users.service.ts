import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environment/environment';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { PaginatedList } from '@shared/models/paginated-list';
import { UserChangePassword } from '../models/user-change-password';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  apiUrl: string = environment.apiUrl;

  constructor(private _http: HttpClient) { }

  getUsers(role: string): Observable<any[]> {
    return this._http.get<any[]>(`${this.apiUrl}users`, {
      params: {
        roleName: role
      }
    });
  }

  createUser(user: User): Observable<string> {
    return this._http.post<string>(`${this.apiUrl}users`, user);
  }

  updateUser(user: User): Observable<User> {
    return this._http.put<User>(`${this.apiUrl}users`, user);
  }

  updateUserPassword(user: UserChangePassword) {
    return this._http.put<boolean>(`${this.apiUrl}users/changePassword`, user);
  }

  deleteUser(id: string): Observable<string> {
    return this._http.delete<string>(`${this.apiUrl}users${id}`);
  }

  getUser(id: number): Observable<User> {
    return this._http.get<User>(`${this.apiUrl}users/${id}`);
  }

  getUsersPagination(pageSize: number, pageNumber: number, name: string = '') {
    return this._http.get<PaginatedList<User>>(`${this.apiUrl}users/pagination`, {
      params: {
        pageNumber: pageNumber,
        pageSize: pageSize,
        name: name
      }
    })
  }
}
