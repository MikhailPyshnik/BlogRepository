import { Component} from '@angular/core';
import { BlogPostService } from './service/blog-post.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  title = 'BlogApiNG';
  userName:string;

  constructor(private blogPostService: BlogPostService) {}

  logout()
  {
    localStorage.removeItem('auth_token');
    localStorage.removeItem('user_name');
  }

  getUserName()
  {
    this.userName = this.blogPostService.getUserName();
  }

  public get authenticated(): boolean 
  {
    return (localStorage.getItem('auth_token') !== null);
  }
}
