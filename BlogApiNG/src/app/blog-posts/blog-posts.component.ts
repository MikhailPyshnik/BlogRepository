import { Component, OnInit, Input} from '@angular/core';
import { Observable } from 'rxjs';
import { BlogPostService } from '../service/blog-post.service';
import { BlogPost } from '../model/blogpost';

@Component({
  selector: 'app-blog-posts',
  templateUrl: './blog-posts.component.html',
  styleUrls: ['./blog-posts.component.css']
})

export class BlogPostsComponent implements OnInit {
  blogPosts: Observable<BlogPost[]>;
  searchString: string;
  isSearch:boolean = false;

  constructor(private blogPostService: BlogPostService) {
  }

  ngOnInit() {
    this.loadBlogPosts();
  }

  loadBlogPosts() {
     this.blogPosts = this.blogPostService.getBlogPosts();
     this.isSearch = false;
  }

  searchBlogs(search:String) {
    this.blogPosts = this.blogPostService.searchBlogPost(search);
    this.isSearch = true;
  }
}