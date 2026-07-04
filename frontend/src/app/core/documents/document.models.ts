export interface DocumentTypeResponse {
  id: number;
  name: string;
  description: string;
  requiredForAll: boolean;
  requiredForDepartmentId?: number | null;
}

export interface CreateDocumentTypeRequest {
  name: string;
  description: string;
  requiredForAll: boolean;
  requiredForDepartmentId?: number | null;
}

export interface DocumentResponse {
  id: number;
  accountId: number;
  documentTypeId: number;
  documentTypeName: string;
  documentTypeDescription: string;
  fileUrl?: string | null;
  status: string;
}

export interface RequireDocumentForEmployeeRequest {
  documentTypeId: number;
}
