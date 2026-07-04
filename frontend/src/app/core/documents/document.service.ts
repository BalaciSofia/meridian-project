import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  CreateDocumentTypeRequest,
  DocumentResponse,
  DocumentTypeResponse,
  RequireDocumentForEmployeeRequest,
} from './document.models';

@Injectable({
  providedIn: 'root',
})
export class DocumentService {
  private readonly apiUrl = 'https://localhost:7166/api/documents';

  constructor(private http: HttpClient) {}

  getDocumentTypes(): Observable<DocumentTypeResponse[]> {
    return this.http.get<DocumentTypeResponse[]>(`${this.apiUrl}/types`);
  }

  createDocumentType(request: CreateDocumentTypeRequest): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/types`, request);
  }

  getMyDocuments(): Observable<DocumentResponse[]> {
    return this.http.get<DocumentResponse[]>(`${this.apiUrl}/my`);
  }

  uploadDocument(documentId: number, file: File): Observable<void> {
    const formData = new FormData();
    formData.append('file', file);

    return this.http.post<void>(`${this.apiUrl}/${documentId}/upload`, formData);
  }

  getDocumentsForEmployee(accountId: number): Observable<DocumentResponse[]> {
    return this.http.get<DocumentResponse[]>(`${this.apiUrl}/employee/${accountId}`);
  }

  requireDocumentForEmployee(
    accountId: number,
    request: RequireDocumentForEmployeeRequest,
  ): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/employee/${accountId}`, request);
  }
}
