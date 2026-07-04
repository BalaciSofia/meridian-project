import { Component, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { forkJoin } from 'rxjs';
import { EmployeeService } from '../../core/employees/employee.service';
import { DepartmentResponse, RoleResponse } from '../../core/lookups/lookup.models';
import { LookupService } from '../../core/lookups/lookup.service';

@Component({
  selector: 'app-new-employee',
  imports: [FormsModule],
  templateUrl: './new-employee.html',
  styleUrl: './new-employee.scss',
})
export class NewEmployee implements OnInit {
  roles = signal<RoleResponse[]>([]);
  departments = signal<DepartmentResponse[]>([]);
  successMessage = signal('');
  errorMessage = signal('');
  isSaving = signal(false);

  email = '';
  firstName = '';
  lastName = '';
  password = '';
  roleId: number | null = null;
  departmentId: number | null = null;

  constructor(
    private lookupService: LookupService,
    private employeeService: EmployeeService,
  ) {}

  ngOnInit(): void {
    forkJoin({
      roles: this.lookupService.getRoles(),
      departments: this.lookupService.getDepartments(),
    }).subscribe({
      next: ({ roles, departments }) => {
        this.roles.set(roles);
        this.departments.set(departments);
        this.roleId = roles[0]?.id ?? null;
        this.departmentId = departments[0]?.id ?? null;
      },
      error: () => {
        this.errorMessage.set('Could not load roles and departments.');
      },
    });
  }

  createEmployee(): void {
    this.successMessage.set('');
    this.errorMessage.set('');

    if (this.roleId === null || this.departmentId === null) {
      this.errorMessage.set('Role and department are required.');
      return;
    }

    this.isSaving.set(true);

    this.employeeService
      .createEmployee({
        email: this.email,
        firstName: this.firstName,
        lastName: this.lastName,
        password: this.password,
        roleId: this.roleId,
        departmentId: this.departmentId,
        mustChangePassword: true,
      })
      .subscribe({
        next: () => {
          this.isSaving.set(false);
          this.successMessage.set('Employee account created.');
          this.resetForm();
        },
        error: () => {
          this.isSaving.set(false);
          this.errorMessage.set('Could not create employee account.');
        },
      });
  }

  private resetForm(): void {
    this.email = '';
    this.firstName = '';
    this.lastName = '';
    this.password = '';
    this.roleId = this.roles()[0]?.id ?? null;
    this.departmentId = this.departments()[0]?.id ?? null;
  }
}
