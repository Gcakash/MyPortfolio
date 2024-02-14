import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FeedbackModel } from '../../Models/Feedback.model';
import { ApiService } from '../ApiService/api.service';
import { HttpClient } from '@angular/common/http';
import { ContactModel } from '../../Models/Contact.model';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService extends ApiService {

  constructor( http: HttpClient) 
  {
    super(http);
  }
  CreateFeedback(feedback: FeedbackModel): Observable<FeedbackModel> {
    return this.post<FeedbackModel>('Feedback', feedback);
  }

  GetContact(): Observable<ContactModel[]> {
    return this.get<ContactModel[]>('Contact/filter');
  }

  // GetFeedback(id: number): Observable<FeedbackModel> {
  //   return this.get<FeedbackModel>(`feedbacks/${id}`);
  // }



  // UpdateFeedback(feedback: FeedbackModel): Observable<FeedbackModel> {
  //   return this.put<FeedbackModel>(`feedbacks/${feedback.Id}`, feedback);
  // }

  // DeleteFeedback(id: number): Observable<any> {
  //   return this.delete<any>(`feedbacks/${id}`);
  // }
}
