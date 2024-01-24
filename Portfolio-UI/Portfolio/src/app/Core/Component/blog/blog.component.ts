import { Component } from '@angular/core';

@Component({
  selector: 'app-blog',
  standalone: true,
  imports: [],
  templateUrl: './blog.component.html',
  styleUrl: './blog.component.scss'
})

export class BlogComponent {
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
