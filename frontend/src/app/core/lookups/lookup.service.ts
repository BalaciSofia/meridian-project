import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DepartmentResponse, RoleResponse } from './lookup.models';

@Injectable({
  providedIn: 'root',
})
export class LookupService {
  private readonly apiUrl = 'https://localhost:7166/api/lookups';

  constructor(private http: HttpClient) {}

  getRoles(): Observable<RoleResponse[]> {
    return this.http.get<RoleResponse[]>(`${this.apiUrl}/roles`);
  }

  getDepartments(): Observable<DepartmentResponse[]> {
    return this.http.get<DepartmentResponse[]>(`${this.apiUrl}/departments`);
  }
}

