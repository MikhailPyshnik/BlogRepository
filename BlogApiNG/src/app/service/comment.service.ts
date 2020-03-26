import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable} from 'rxjs';
import { AddCommentBlog} from '../model/addCommentBlog ';
import { CommentBlog} from '../model/commentBlog';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  Url: string;
  userName: string; 

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };

  constructor(private http: HttpClient) {
      this.Url = 'https://localhost:44338/api/comment/';
  }

  getCommnetById(postId:string, commentId:string)
  {
    return this.http.get<CommentBlog>(`${this.Url}${postId}/${commentId}`);
  }

  addComment(blogId: string, comment ): Observable<AddCommentBlog> {
    if(this.authorization())
    {
      return this.http.post<AddCommentBlog>(`${this.Url}${blogId}`, JSON.stringify(comment), this.httpOptions);
    }
  }

  updateComment(postId:string, commentId:string, comment): Observable<AddCommentBlog> {
    if(this.authorization())
    {
      return this.http.put<AddCommentBlog>(`${this.Url}${postId}/${commentId}`, JSON.stringify(comment), this.httpOptions);
    }
  }

  deleteComment(blogId,commentId): Observable<Object> {
    if(this.authorization())
    {
      return this.http.delete<Object>(`${this.Url}${blogId}/${commentId}`, this.httpOptions);
    }
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
        return "Hello"+ localStorage.user_name;
    }
    else return "Please login!";
  }
}