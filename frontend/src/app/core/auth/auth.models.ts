export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  accountId: number;
  email: string;
  role: string;
  department: string;
  mustChangePassword: boolean;
}

export interface ChangePasswordRequest {
  currentPassword: string;
  newPassword: string;
}

