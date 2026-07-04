import { Component, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DocumentTypeResponse } from '../../core/documents/document.models';
import { DocumentService } from '../../core/documents/document.service';
import { DepartmentResponse } from '../../core/lookups/lookup.models';
import { LookupService } from '../../core/lookups/lookup.service';

@Component({
  selector: 'app-document-types',
  imports: [FormsModule],
  templateUrl: './document-types.html',
  styleUrl: './document-types.scss',
})
export class DocumentTypes implements OnInit {
  documentTypes = signal<DocumentTypeResponse[]>([]);
  departments = signal<DepartmentResponse[]>([]);
  errorMessage = signal('');
  successMessage = signal('');
  isLoading = signal(true);
  isSaving = signal(false);

  name = '';
  description = '';
  requiredForAll = true;
  requiredForDepartmentId: number | null = null;

  constructor(
    private documentService: DocumentService,
    private lookupService: LookupService,
  ) {}

  ngOnInit(): void {
    this.loadPageData();
  }

  createDocumentType(): void {
    this.errorMessage.set('');
    this.successMessage.set('');
    this.isSaving.set(true);

    this.documentService
      .createDocumentType({
        name: this.name,
        description: this.description,
        requiredForAll: this.requiredForAll,
        requiredForDepartmentId: this.requiredForAll ? null : this.requiredForDepartmentId,
      })
      .subscribe({
        next: () => {
          this.isSaving.set(false);
          this.successMessage.set('Document type created.');
          this.resetForm();
          this.loadDocumentTypes();
        },
        error: () => {
          this.isSaving.set(false);
          this.errorMessage.set('Could not create document type.');
        },
      });
  }

  getDepartmentName(departmentId?: number | null): string {
    if (!departmentId) {
      return 'All departments';
    }

    return this.departments().find((department) => department.id === departmentId)?.departmentName ?? 'Department';
  }

  private loadPageData(): void {
    this.loadDocumentTypes();

    this.lookupService.getDepartments().subscribe({
      next: (departments) => {
        this.departments.set(departments);
        this.requiredForDepartmentId = departments[0]?.id ?? null;
      },
      error: () => {
        this.errorMessage.set('Could not load departments.');
      },
    });
  }

  private loadDocumentTypes(): void {
    this.isLoading.set(true);

    this.documentService.getDocumentTypes().subscribe({
      next: (documentTypes) => {
        this.documentTypes.set(documentTypes);
        this.isLoading.set(false);
      },
      error: () => {
        this.errorMessage.set('Could not load document types.');
        this.isLoading.set(false);
      },
    });
  }

  private resetForm(): void {
    this.name = '';
    this.description = '';
    this.requiredForAll = true;
    this.requiredForDepartmentId = this.departments()[0]?.id ?? null;
  }
}

