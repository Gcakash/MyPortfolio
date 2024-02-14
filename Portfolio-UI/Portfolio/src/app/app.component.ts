import { BrowserModule } from '@angular/platform-browser';
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './Core/Component/navbar/navbar.component';
import { FooterComponent } from "./Core/Component/footer/footer.component";
import { HomeComponent } from './Core/Component/home/home.component';

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
    imports: [CommonModule, RouterOutlet, NavbarComponent,
       FooterComponent,]
})
export class AppComponent {
  title = 'Portfolio';
}
