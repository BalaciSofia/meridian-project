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
