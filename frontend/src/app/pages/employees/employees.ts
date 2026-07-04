import { Component, OnInit, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { EmployeeResponse } from '../../core/employees/employee.models';
import { EmployeeService } from '../../core/employees/employee.service';

@Component({
  selector: 'app-employees',
  imports: [RouterLink],
  templateUrl: './employees.html',
  styleUrl: './employees.scss',
})
export class Employees implements OnInit {
  employees = signal<EmployeeResponse[]>([]);
  errorMessage = signal('');
  isLoading = signal(true);

  constructor(private employeeService: EmployeeService) {}

  ngOnInit(): void {
    this.employeeService.getEmployees().subscribe({
      next: (employees) => {
        this.employees.set(employees);
        this.isLoading.set(false);
      },
      error: () => {
        this.errorMessage.set('Could not load employees.');
        this.isLoading.set(false);
      },
    });
  }
}
