import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { BlogPostService } from '../service/blog-post.service';
import { AddArticle } from '../model/addArticle';

@Component({
  selector: 'app-blog-post-add-edit',
  templateUrl: './blog-post-add-edit.component.html',
  styleUrls: ['./blog-post-add-edit.component.css']
})

export class BlogPostAddEditComponent implements OnInit {
  form: FormGroup;
  actionType: string;
  formTitle: string;
  formBody: string;
  formCategory:string;
  postId: string;
  errorMessage: any;
  existingArticlePost: AddArticle;

  category:string = "None";
  categorys: string[] =
  [
  "None",
  "Politics",
  "Health",
  "Economy",
  "Technology",
  "IT",
  "Sport"
  ];

  constructor(private fb: FormBuilder,private blogPostService: BlogPostService, private formBuilder: FormBuilder, private avRoute: ActivatedRoute, private router: Router) {
    const idParam = 'id';
    this.actionType = 'Add';
    this.formTitle = 'title';
    this.formBody = 'body';
    this.formCategory = 'addCategory';
    if (this.avRoute.snapshot.params[idParam]) {
      this.postId = this.avRoute.snapshot.params[idParam];
    }

    this.form = this.formBuilder.group(
      {
        postId: 0,
        title: [''],
        body: [''],
        addCategory: [''],
      }
    )
  }

  ngOnInit() {
    if (this.postId != null) {
      this.actionType = 'Edit';
      this.blogPostService.getBlogPost(this.postId)
        .subscribe(data => (
          this.form.controls[this.formTitle].setValue(data.title),
          this.form.controls[this.formBody].setValue(data.text),
          this.form.controls[this.formCategory].setValue(data.category)
        ));
    }
  }

  save() {
    if (!this.form.valid) {
      return;
    }

    if (this.actionType === 'Add') {
      let blogPost: AddArticle = {
        category: this.form.get(this.formCategory).value,
        title: this.form.get(this.formTitle).value,
        text: this.form.get(this.formBody).value
      };
      this.blogPostService.saveBlogPost(blogPost)
        .subscribe((data) => {
          this.router.navigate(['/blogpost', data.id]);
        });
    }

    if (this.actionType === 'Edit') {
      let blogPost: AddArticle = {
        category: this.form.get(this.formCategory).value,
        title: this.form.get(this.formTitle).value,
        text: this.form.get(this.formBody).value
      };
      this.blogPostService.updateBlogPost(this.postId, blogPost)
        .subscribe((data) => {
          this.router.navigate(['/blogpost', data.id]);
        });
    }
  }

  cancel() {
    this.router.navigate(['/']);
  }

  public get logIn(): boolean {
    return (localStorage.getItem('auth_token') !== null);
  }

  get title() { return this.form.get(this.formTitle); }
  get body() { return this.form.get(this.formBody); }
}