import { Component, OnInit, signal } from '@angular/core';
import { AuthService } from '../../core/auth/auth.service';
import { DocumentResponse } from '../../core/documents/document.models';
import { DocumentService } from '../../core/documents/document.service';

@Component({
  selector: 'app-profile',
  imports: [],
  templateUrl: './profile.html',
  styleUrl: './profile.scss',
})
export class Profile implements OnInit {
  private readonly fileBaseUrl = 'https://localhost:7166';

  documents = signal<DocumentResponse[]>([]);
  errorMessage = signal('');
  successMessage = signal('');
  isLoading = signal(true);
  uploadingDocumentId = signal<number | null>(null);

  constructor(
    public authService: AuthService,
    private documentService: DocumentService,
  ) {}

  ngOnInit(): void {
    this.loadDocuments();
  }

  uploadDocument(document: DocumentResponse, event: Event): void {
    const input = event.target as HTMLInputElement;
    const file = input.files?.[0];

    if (!file) {
      return;
    }

    this.errorMessage.set('');
    this.successMessage.set('');
    this.uploadingDocumentId.set(document.id);

    this.documentService.uploadDocument(document.id, file).subscribe({
      next: () => {
        this.successMessage.set('Document uploaded.');
        this.uploadingDocumentId.set(null);
        input.value = '';
        this.loadDocuments();
      },
      error: () => {
        this.errorMessage.set('Could not upload document.');
        this.uploadingDocumentId.set(null);
        input.value = '';
      },
    });
  }

  getFileUrl(fileUrl: string): string {
    if (fileUrl.startsWith('http')) {
      return fileUrl;
    }

    return `${this.fileBaseUrl}${fileUrl}`;
  }

  private loadDocuments(): void {
    this.isLoading.set(true);

    this.documentService.getMyDocuments().subscribe({
      next: (documents) => {
        this.documents.set(documents);
        this.isLoading.set(false);
      },
      error: () => {
        this.errorMessage.set('Could not load documents.');
        this.isLoading.set(false);
      },
    });
  }
}
