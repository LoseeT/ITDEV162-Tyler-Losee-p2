// src/app/services/post.service.ts
import { Injectable } from '@angular/core';
import { Post } from '../models/post.model';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  private posts: Post[] = [];
  private nextId = 1; // To generate unique IDs for each post

  getPosts(): Post[] {
    return this.posts;
  }

  addPost(post: Post): void {
    post.id = this.nextId++;
    this.posts.push(post);
  }
}
