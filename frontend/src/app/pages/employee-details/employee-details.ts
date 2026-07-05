import { Component, OnInit, signal } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { forkJoin } from 'rxjs';
import { DocumentResponse, DocumentTypeResponse } from '../../core/documents/document.models';
import { DocumentService } from '../../core/documents/document.service';
import { EmployeeResponse } from '../../core/employees/employee.models';
import { EmployeeService } from '../../core/employees/employee.service';

@Component({
  selector: 'app-employee-details',
  imports: [RouterLink, FormsModule],
  templateUrl: './employee-details.html',
  styleUrl: './employee-details.scss',
})
export class EmployeeDetails implements OnInit {
  accountId: number | null = null;
  employee = signal<EmployeeResponse | null>(null);
  documents = signal<DocumentResponse[]>([]);
  documentTypes = signal<DocumentTypeResponse[]>([]);
  errorMessage = signal('');
  isLoading = signal(true);
  isSavingDocument = signal(false);
  updatingDocumentId = signal<number | null>(null);
  selectedDocumentTypeId: number | null = null;

  constructor(
    private route: ActivatedRoute,
    private employeeService: EmployeeService,
    private documentService: DocumentService,
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    if (!id) {
      this.errorMessage.set('Invalid employee id.');
      this.isLoading.set(false);
      return;
    }

    this.accountId = id;

    forkJoin({
      employee: this.employeeService.getEmployeeById(id),
      documents: this.documentService.getDocumentsForEmployee(id),
      documentTypes: this.documentService.getDocumentTypes(),
    }).subscribe({
      next: ({ employee, documents, documentTypes }) => {
        this.employee.set(employee);
        this.documents.set(documents);
        this.documentTypes.set(documentTypes);
        this.selectedDocumentTypeId = documentTypes[0]?.id ?? null;
        this.isLoading.set(false);
      },
      error: () => {
        this.errorMessage.set('Could not load employee.');
        this.isLoading.set(false);
      },
    });
  }

  requireDocument(): void {
    this.errorMessage.set('');

    if (!this.accountId || this.selectedDocumentTypeId === null) {
      this.errorMessage.set('Select a document type.');
      return;
    }

    this.isSavingDocument.set(true);

    this.documentService
      .requireDocumentForEmployee(this.accountId, {
        documentTypeId: this.selectedDocumentTypeId,
      })
      .subscribe({
        next: () => {
          this.isSavingDocument.set(false);
          this.loadEmployeeDocuments();
        },
        error: (error) => {
          this.isSavingDocument.set(false);

          if (error.status === 409) {
            this.errorMessage.set('This document is already required for this employee.');
            return;
          }

          this.errorMessage.set('Could not require document.');
        },
      });
  }

  updateDocumentStatus(document: DocumentResponse, status: 'approved' | 'rejected'): void {
    this.errorMessage.set('');
    this.updatingDocumentId.set(document.id);

    this.documentService.updateDocumentStatus(document.id, { status }).subscribe({
      next: () => {
        this.updatingDocumentId.set(null);
        this.loadEmployeeDocuments();
      },
      error: () => {
        this.errorMessage.set('Could not update document status.');
        this.updatingDocumentId.set(null);
      },
    });
  }

  private loadEmployeeDocuments(): void {
    if (!this.accountId) {
      return;
    }

    this.documentService.getDocumentsForEmployee(this.accountId).subscribe({
      next: (documents) => this.documents.set(documents),
      error: () => this.errorMessage.set('Could not reload documents.'),
    });
  }
}
