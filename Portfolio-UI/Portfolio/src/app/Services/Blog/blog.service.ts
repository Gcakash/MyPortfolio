import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BlogModel } from '../../Models/Blog.model';
import { ApiService } from '../ApiService/api.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BlogService extends ApiService  {

  constructor( http: HttpClient) 
  {
    super(http);
  }
  CreateBlog(Blog: BlogModel): Observable<BlogModel> {
    return this.post<BlogModel>('Blog', Blog);
  }

  GetBlogs(): Observable<BlogModel[]> {
    return this.get<BlogModel[]>('Blog');
  }

  // GetBlog(id: number): Observable<BlogModel> {
  //   return this.get<BlogModel>(`Blogs/${id}`);
  // }



  // UpdateBlog(Blog: BlogModel): Observable<BlogModel> {
  //   return this.put<BlogModel>(`Blogs/${Blog.Id}`, Blog);
  // }

  // DeleteBlog(id: number): Observable<any> {
  //   return this.delete<any>(`Blogs/${id}`);
  // }
}

