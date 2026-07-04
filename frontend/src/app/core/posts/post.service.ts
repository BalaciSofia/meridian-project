import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  CommentResponse,
  CreateCommentRequest,
  CreatePostRequest,
  CreateReactRequest,
  PostResponse,
  ReactResponse,
} from './post.models';

@Injectable({
  providedIn: 'root',
})
export class PostService {
  private readonly apiUrl = 'https://localhost:7166/api/posts';

  constructor(private http: HttpClient) {}

  getPosts(): Observable<PostResponse[]> {
    return this.http.get<PostResponse[]>(this.apiUrl);
  }

  createPost(request: CreatePostRequest): Observable<void> {
    return this.http.post<void>(this.apiUrl, request);
  }

  getComments(postId: number): Observable<CommentResponse[]> {
    return this.http.get<CommentResponse[]>(`${this.apiUrl}/${postId}/comments`);
  }

  addComment(postId: number, request: CreateCommentRequest): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${postId}/comments`, request);
  }

  getReacts(postId: number): Observable<ReactResponse[]> {
    return this.http.get<ReactResponse[]>(`${this.apiUrl}/${postId}/reacts`);
  }

  addReact(postId: number, request: CreateReactRequest): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${postId}/reacts`, request);
  }
}

