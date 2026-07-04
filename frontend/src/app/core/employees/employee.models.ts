export interface CreateEmployeeRequest {
  email: string;
  firstName: string;
  lastName: string;
  password: string;
  roleId: number;
  departmentId: number;
  mustChangePassword: boolean;
}

export interface EmployeeResponse {
  id: number;
  email: string;
  firstName: string;
  lastName: string;
  role: string;
  department: string;
  mustChangePassword: boolean;
}
