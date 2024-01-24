import { CommonModule } from '@angular/common';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import Typed from 'typed.js';

@Component({
  selector: 'app-home',
  standalone: true,

  imports: [CommonModule,CarouselModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {
  carouselItems = [
    { title: 'Title1', subtitle: 'Auto-typing effect in Angular!' },
    { title: 'Title2', subtitle: 'Auto-typing' },

  ];

  ngOnInit() {
    this.initTyped();
  }

  initTyped() {
    const options = {
      strings: ['Software Engineer', 'Dot Net Developer','Lecturer','Computer Engineer'],
      typeSpeed: 50,
      backSpeed: 25,
      backDelay: 1500,
      startDelay: 500,
      loop: true
    };

    new Typed('.typed-text', options);
  }
}
// export class HomeComponent {
//   carouselItems = [
//     { title: 'Slide 1', image: '/assets/img/img1.png', subtitle:"This is the subTiltle of the image ho hsahjjh hdsjhdcc " },
//     { title: 'Slide 2', image: '/assets/img/img2.png', subtitle:"This is the subTiltle of the image ho hsahjjh hdsjhdcc " },
//     { title: 'Slide 3', image: '/assets/img/img3.png', subtitle:"This is the subTiltle of the image ho hsahjjh hdsjhdcc " },
//   ];
// }



