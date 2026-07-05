export interface PostResponse {
  id: number;
  title: string;
  content: string;
  imageUrl?: string | null;
  createdAt: string;
  createdByAccountId: number;
  createdByName: string;
}

export interface CreatePostRequest {
  title: string;
  content: string;
  imageUrl?: string | null;
}

export interface CommentResponse {
  id: number;
  postId: number;
  accountId: number;
  content: string;
  createdAt: string;
  authorName: string;
}

export interface CreateCommentRequest {
  content: string;
}

export interface ReactResponse {
  id: number;
  postId: number;
  accountId: number;
  reactType: string;
}

export interface CreateReactRequest {
  reactType: string;
}

export interface PostViewModel extends PostResponse {
  comments: CommentResponse[];
  reacts: ReactResponse[];
  newComment: string;
  isLoadingDetails: boolean;
  isCommentFormOpen: boolean;
}
