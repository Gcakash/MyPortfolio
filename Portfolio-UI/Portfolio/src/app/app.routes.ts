import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './Core/Component/about/about.component';
import { HomeComponent } from './Core/Component/home/home.component';
import { PageNotFoundComponent } from './Core/Component/page-not-found/page-not-found.component';
import { ContactComponent } from './Core/Component/contact/ContactComponent';
import { BlogComponent } from './Core/Component/blog/blog.component';

export const routes: Routes = [
    //{ path: '', pathMatch: 'full', redirectTo: 'contact-us' }
    { path: 'about', component: AboutComponent },
    { path: 'home', component: HomeComponent },
    { path :'contact', component:ContactComponent},
    { path :'blog', component:BlogComponent},
    { path: '', component: HomeComponent }, //for url
    { path: '**', component: PageNotFoundComponent },//for worng url
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
  })
  export class AppRoutingModule { }