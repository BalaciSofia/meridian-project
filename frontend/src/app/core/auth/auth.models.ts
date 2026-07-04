export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  accountId: number;
  email: string;
  firstName: string;
  lastName: string;
  role: string;
  department: string;
  departmentSlackLink: string;
  mustChangePassword: boolean;
}

export interface ChangePasswordRequest {
  currentPassword: string;
  newPassword: string;
}

export interface CurrentAccountResponse {
  id: number;
  email: string;
  firstName: string;
  lastName: string;
  role: string;
  department: string;
  departmentSlackLink: string;
  mustChangePassword: boolean;
}
