import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable} from 'rxjs';
import { BlogPost } from '../model/blogpost';

@Injectable({
  providedIn: 'root'
})
export class BlogPostService {
  Url: string;

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
    })
  };

  constructor(private http: HttpClient) {
      this.Url = 'https://localhost:44338/api/Blog/';
  }

  getBlogPosts(): Observable<Array<BlogPost>> {
    return this.http.get<Array<BlogPost>>(this.Url + 'GetAllBlogs' );
  }

  getCountPostCommnet(): Observable<Array<BlogPost>> {
    return this.http.get<Array<BlogPost>>(this.Url + 'GetAllBlogs' );
  }

  getBlogPost(postId: string): Observable<BlogPost> {
      return this.http.get<BlogPost>(`${this.Url+'GetBlog/'}${postId}`);
  }

  saveBlogPost(blogPost): Observable<BlogPost> {
      if(this.authorization())
      {
      return this.http.post<BlogPost>(this.Url + 'AddBlog', JSON.stringify(blogPost), this.httpOptions);
      }
  }

  updateBlogPost(postId: string, blogPost): Observable<BlogPost> {
    if(this.authorization())
    {
      return this.http.put<BlogPost>(this.Url + 'UpdateBlog/'+ postId, JSON.stringify(blogPost), this.httpOptions);
    }
  }

  deleteBlogPost(postId: string): Observable<Object> {
    if(this.authorization())
    {
      return this.http.delete<Object>(`${this.Url+'DeleteBlog/'}${postId}`);
    }
  }

  searchBlogPost(serchString): Observable<Array<BlogPost>> {
      return this.http.get<Array<BlogPost>>(this.Url + 'GetSearchBlog/?searchSrting=' + serchString);
  }

  private authorization() {
    if(localStorage.auth_token!= null)
    {
      this.httpOptions.headers = this.httpOptions.headers.set('authorization', 'bearer ' + localStorage.auth_token);
      return true;
    }
    else return false;
  }

  getUserName()
  {
    if(localStorage.user_name!= null)
    {
        return "Hello user: " + localStorage.user_name + "!";
    }
    else return "Please login!"
  }
}