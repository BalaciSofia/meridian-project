import { Component, OnInit, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { forkJoin } from 'rxjs';
import { AuthService } from '../../core/auth/auth.service';
import { PostService } from '../../core/posts/post.service';
import { PostResponse, PostViewModel } from '../../core/posts/post.models';

@Component({
  selector: 'app-home',
  imports: [FormsModule, DatePipe],
  templateUrl: './home.html',
  styleUrl: './home.scss',
})
export class Home implements OnInit {
  posts = signal<PostViewModel[]>([]);
  expandedCommentsPostIds = signal<number[]>([]);
  errorMessage = signal('');
  successMessage = signal('');
  isLoading = signal(true);
  isCreatingPost = signal(false);
  isPostModalOpen = signal(false);

  title = '';
  content = '';
  imageUrl = '';

  constructor(
    public authService: AuthService,
    private postService: PostService,
  ) {}

  ngOnInit(): void {
    this.loadPosts();
  }

  canCreatePost(): boolean {
    return this.authService.isHrOrAdmin();
  }

  openPostModal(): void {
    this.successMessage.set('');
    this.errorMessage.set('');
    this.isPostModalOpen.set(true);
  }

  closePostModal(): void {
    this.isPostModalOpen.set(false);
  }

  createPost(): void {
    this.successMessage.set('');
    this.errorMessage.set('');
    this.isCreatingPost.set(true);

    this.postService
      .createPost({
        title: this.title,
        content: this.content,
        imageUrl: this.imageUrl || null,
      })
      .subscribe({
        next: () => {
          this.title = '';
          this.content = '';
          this.imageUrl = '';
          this.successMessage.set('Post created.');
          this.isCreatingPost.set(false);
          this.closePostModal();
          this.loadPosts();
        },
        error: () => {
          this.errorMessage.set('Could not create post.');
          this.isCreatingPost.set(false);
        },
      });
  }

  addComment(post: PostViewModel): void {
    const content = post.newComment.trim();

    if (!content) {
      return;
    }

    this.postService.addComment(post.id, { content }).subscribe({
      next: () => {
        post.newComment = '';
        this.loadPostDetails(post);
      },
      error: () => {
        this.errorMessage.set('Could not add comment.');
      },
    });
  }

  setReaction(post: PostViewModel, reactType: 'like' | 'dislike'): void {
    this.postService.addReact(post.id, { reactType }).subscribe({
      next: () => this.loadPostDetails(post),
      error: () => {
        this.errorMessage.set('Could not update reaction.');
      },
    });
  }

  getReactionCount(post: PostViewModel, reactType: 'like' | 'dislike'): number {
    return post.reacts.filter((react) => react.reactType === reactType).length;
  }

  hasUserReacted(post: PostViewModel, reactType: 'like' | 'dislike'): boolean {
    const accountId = this.authService.currentUser()?.accountId;

    if (!accountId) {
      return false;
    }

    return post.reacts.some((react) => react.accountId === accountId && react.reactType === reactType);
  }

  getVisibleComments(post: PostViewModel) {
    if (this.areCommentsExpanded(post.id)) {
      return post.comments;
    }

    return post.comments.slice(0, 3);
  }

  hasMoreComments(post: PostViewModel): boolean {
    return post.comments.length > 3;
  }

  areCommentsExpanded(postId: number): boolean {
    return this.expandedCommentsPostIds().includes(postId);
  }

  toggleComments(postId: number): void {
    const expandedIds = this.expandedCommentsPostIds();

    if (expandedIds.includes(postId)) {
      this.expandedCommentsPostIds.set(expandedIds.filter((id) => id !== postId));
      return;
    }

    this.expandedCommentsPostIds.set([...expandedIds, postId]);
  }

  private loadPosts(): void {
    this.isLoading.set(true);
    this.errorMessage.set('');

    this.postService.getPosts().subscribe({
      next: (posts) => {
        const sortedPosts = [...posts].sort((firstPost, secondPost) =>
            new Date(secondPost.createdAt).getTime() - new Date(firstPost.createdAt).getTime(),
        );
        const viewModels = sortedPosts.map((post) => this.toViewModel(post));
        this.posts.set(viewModels);
        this.isLoading.set(false);
        viewModels.forEach((post) => this.loadPostDetails(post));
      },
      error: () => {
        this.errorMessage.set('Could not load posts.');
        this.isLoading.set(false);
      },
    });
  }

  private loadPostDetails(post: PostViewModel): void {
    post.isLoadingDetails = true;

    forkJoin({
      comments: this.postService.getComments(post.id),
      reacts: this.postService.getReacts(post.id),
    }).subscribe({
      next: ({ comments, reacts }) => {
        post.comments = comments;
        post.reacts = reacts;
        post.isLoadingDetails = false;
        this.posts.set([...this.posts()]);
      },
      error: () => {
        post.isLoadingDetails = false;
        this.posts.set([...this.posts()]);
      },
    });
  }

  private toViewModel(post: PostResponse): PostViewModel {
    return {
      ...post,
      comments: [],
      reacts: [],
      newComment: '',
      isLoadingDetails: false,
    };
  }
}
