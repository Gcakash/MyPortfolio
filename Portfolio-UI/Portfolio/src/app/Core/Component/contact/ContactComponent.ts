import { Component, Inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { FeedbackModel } from '../../../Models/Feedback.model';
import { FeedbackService } from '../../../Services/Feedback/feedback.service';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ContactModel } from '../../../Models/Contact.model';


@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.scss'
})
export class ContactComponent implements OnInit {
  errorMessage: string | null = null;
  successMessage: string | null = null;
  http = Inject(HttpClient);
  model: FeedbackModel = { Id: 0, FullName: '', Email: '', Title: '', Phone: '', Message: '' };
  contact: ContactModel = {Id : 0, FullName: '', Email: 'akashgc2054@gmail.com', FullAddress1: 'Kathmanu-13,Nepal', Phone: '9867288224', FullAddress2: '',IsActive :true,Country :''};
  constructor(private feedbackService: FeedbackService) {
  }
  ngOnInit(): void {
    this.getContacts();
  }




  // async onFormSubmit(): Promise<void> {
  //   try {
  //     const response: any = await this.feedbackService.CreateFeedback(this.model).toPromise();
  //     if (response.Type === 'SUCCESS') {
  //       console.log('Feedback added successfully!');
  //       this.successMessage = 'Feedback added successfully!';
  //       this.resetForm();
  //     } else {
  //       console.error('Error:', response.Errors);
  //       this.errorMessage = 'Failed to add feedback. Please try again later.';
  //     }
  //   } catch (error) {
  //     console.error('HTTP Error:', error);
  //     this.errorMessage = 'An error occurred while processing your request. Please try again later.';
  //   }
  // }


  onFormSubmit(): void {
    this.successMessage =null;
    this.errorMessage = null;
    this.feedbackService.CreateFeedback(this.model)
      .subscribe((response: any) => {
        if (response.type === 0) {
          console.log('Feedback added successfully!');
          this.successMessage = 'Thank you for your feedback. We will consider it.';
          this.resetForm();
        } else {
          console.error('Error:', response.Errors);
          this.errorMessage = 'Failed to add feedback. Please try again later.';
        }  
      });
  }
  resetForm(): void {
    this.model = { Id: 0, FullName: '', Email: '', Title: '', Phone: '', Message: '' };
  }

  getContacts(): void {
    this.feedbackService.GetContact()
    .subscribe((response: any) => {
      if (response.type === 0)
       {
        console.log('Data Getting successfully!');
        if (response.result !== undefined && response.result !== null)
         {
        this.contact = {
          FullAddress1: response.result.fullAddress1,
          Email : response.result.email,
          Phone : response.result.phone,
          Id:1, 
          FullName : '',
          Country : '', 
          FullAddress2 : '', 
          IsActive : true
        }
      }

      } else {
        console.error('Error:', response.Errors);
        this.errorMessage = 'Failed to Get Latest Contact';
      }  
    });

  }
}
