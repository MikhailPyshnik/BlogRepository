import { Component, OnInit } from '@angular/core';
import { Router,ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BlogPostService } from '../service/blog-post.service';
import { AddArticle } from '../model/addArticle';
import { CommentService } from '../service/comment.service';
import { BlogPost } from '../model/blogpost';
import { AddCommentBlog} from '../model/addCommentBlog ';
import { CommentBlog} from '../model/commentBlog';
import { ViewportScroller } from '@angular/common';

@Component({
  selector: 'app-blog-post',
  templateUrl: './blog-post.component.html',
  styleUrls: ['./blog-post.component.scss']
})

export class BlogPostComponent implements OnInit {
  blogPostss$: Observable<BlogPost[]>;
  blogPost$: Observable<BlogPost>;
  postId: string;
  autUser: boolean = false;
  user:string;
  blogId:string;
  commentId:string;

  commentEdit : CommentBlog;
  isCommentEdit:boolean=false;

  textComment: string;
  form: FormGroup;
  actionType: string;
  formTitle: string;
  formBody: string;
  errorMessage: any;
  existingArticlePost: AddArticle;

  constructor(private viewportScroller: ViewportScroller, private blogPostService: BlogPostService,private commentService: CommentService, private formBuilder: FormBuilder, private avRoute: ActivatedRoute, private router: Router) {
    const idParam = 'id';
    if (this.avRoute.snapshot.params[idParam]) {
      this.postId = this.avRoute.snapshot.params[idParam];
    }
  }

  ngOnInit() {
    this.loadBlogPost();
    this.login();
  }

  loadBlogPost() {
    this.blogPost$ = this.blogPostService.getBlogPost(this.postId);
  }

  loadBlogPosts() {
    this.blogPostss$ = this.blogPostService.getBlogPosts();
  }

  login()
  {
    if(localStorage.getItem('auth_token') !== null)
    {
      this.user = localStorage.getItem('user_name');
    }
  }

  delete(postId) {
    const ans = confirm('Do you want to delete blog post with id: ' + postId);
    if (ans) {
      this.blogPostService.deleteBlogPost(postId).subscribe((data) => {
          this.loadBlogPosts();
          this.router.navigate(['/']);
      });
    }
  }

  save() {
  let comment: AddCommentBlog = {
    text: this.textComment
  };
  this.commentService.addComment(this.postId, comment)
    .subscribe((data) => {
      window.location.reload();
    });
  }

  editComment(postId:string, commentId:string)
  {
    this.commentId =commentId;
    this.commentService.getCommnetById(postId,commentId)
            .subscribe(data => {
                  this.commentEdit = data;
                  this.textComment = data.text;
              });
    this.isCommentEdit =  true;
    
    this.viewportScroller.scrollToAnchor("123");
  } 

  updateComment(postId: string){
    debugger;
    let comment: AddCommentBlog = {
      text: this.textComment
    };
    this.commentService.updateComment(postId,this.commentId, comment)
      .subscribe((data) => {
        window.location.reload();
      });
      this.isCommentEdit =  false;
      }

  cancel() {
     if(this.isCommentEdit)
     {
      this.isCommentEdit = false;
      this.textComment = "";
     }
    else{
      this.router.navigate(['/']);}

  }

  deleteComment(blogId,commentId,commentText) {
    const ans = confirm('Do you want to delete comment post with id: ' + commentText);
    if (ans) {
      this.commentService.deleteComment(blogId,commentId).subscribe((data) => {
          this.loadBlogPosts();
          window.location.reload();
      });
    }
  }

  public get logIn(): boolean {
    return (localStorage.getItem('auth_token') !== null);
  }

  get body() { return this.form.get(this.formBody); }
}
