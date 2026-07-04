import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateEmployeeRequest } from './employee.models';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  private readonly apiUrl = 'https://localhost:7166/api/auth';

  constructor(private http: HttpClient) {}

  createEmployee(request: CreateEmployeeRequest): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/new-employee`, request);
  }
}

