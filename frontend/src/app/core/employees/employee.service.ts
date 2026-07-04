import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateEmployeeRequest, EmployeeResponse } from './employee.models';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  private readonly authApiUrl = 'https://localhost:7166/api/auth';
  private readonly employeesApiUrl = 'https://localhost:7166/api/employees';

  constructor(private http: HttpClient) {}

  createEmployee(request: CreateEmployeeRequest): Observable<void> {
    return this.http.post<void>(`${this.authApiUrl}/new-employee`, request);
  }

  getEmployees(): Observable<EmployeeResponse[]> {
    return this.http.get<EmployeeResponse[]>(`${this.employeesApiUrl}/employees`);
  }

  getEmployeeById(id: number): Observable<EmployeeResponse> {
    return this.http.get<EmployeeResponse>(`${this.employeesApiUrl}/${id}`);
  }
}
