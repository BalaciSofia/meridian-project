export interface CreateEmployeeRequest {
  email: string;
  firstName: string;
  lastName: string;
  password: string;
  roleId: number;
  departmentId: number;
  mustChangePassword: boolean;
}

