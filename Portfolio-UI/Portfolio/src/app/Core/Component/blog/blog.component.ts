import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { BlogModel } from '../../../Models/Blog.model';
import { BlogService } from '../../../Services/Blog/blog.service';

@Component({
  selector: 'app-blog',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './blog.component.html',
  styleUrl: './blog.component.scss'
})

export class BlogComponent implements OnInit {

//blogs: BlogModel = { blogId: 0, title: '', content: '', image: '', datePublished: new Date(), isActive: true };

  blogs: BlogModel[] = [];

  constructor(private blogService: BlogService) { }

  ngOnInit(): void {
    this.loadBlogs();
  }

  loadBlogs() {
    this.blogService.GetBlogs()
      .subscribe((response: any) => {
        if (response.type === 0) {
        console.log('Feedback added successfully!');
            this.blogs = response.result;
          
        } 
        else {
          console.error('Error:', response.Errors);
        }  
      });
  }


  
  blogPosts = [
    {
      title: 'Example Post 1',
      date: new Date(),
      imageUrl: 'https://placekitten.com/800/400', 
      content: 'This is the content of example post 1.'
    },
    {
      title: 'Example Post 2',
      date: new Date(),
      imageUrl: 'https://placekitten.com/800/401', 
      content: 'This is the content of example post 2.'
    },
    {
      title: 'Example Post 3',
      date: new Date(),
      imageUrl: 'https://placekitten.com/800/402', 
      content: 'This is the content of example post 3.'
    },
    {
      title: 'Example Post 4',
      date: new Date(),
      imageUrl: 'https://placekitten.com/800/403', 
      content: 'This is the content of example post 4.'
    }
  ];
}
