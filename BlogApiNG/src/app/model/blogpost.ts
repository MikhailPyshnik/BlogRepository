import { CommentBlog } from '../model/commentBlog';

export class BlogPost {
    id: string;
    userName: string;
    title: string;
    text: string;
    craetedOn: Date;
    updateOn: Date;
    category: string;
    commets: Array<CommentBlog>;
  }