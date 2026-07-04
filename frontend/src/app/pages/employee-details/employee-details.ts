import { Component, OnInit, signal } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { EmployeeResponse } from '../../core/employees/employee.models';
import { EmployeeService } from '../../core/employees/employee.service';

@Component({
  selector: 'app-employee-details',
  imports: [RouterLink],
  templateUrl: './employee-details.html',
  styleUrl: './employee-details.scss',
})
export class EmployeeDetails implements OnInit {
  employee = signal<EmployeeResponse | null>(null);
  errorMessage = signal('');
  isLoading = signal(true);

  constructor(
    private route: ActivatedRoute,
    private employeeService: EmployeeService,
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    if (!id) {
      this.errorMessage.set('Invalid employee id.');
      this.isLoading.set(false);
      return;
    }

    this.employeeService.getEmployeeById(id).subscribe({
      next: (employee) => {
        this.employee.set(employee);
        this.isLoading.set(false);
      },
      error: () => {
        this.errorMessage.set('Could not load employee.');
        this.isLoading.set(false);
      },
    });
  }
}
