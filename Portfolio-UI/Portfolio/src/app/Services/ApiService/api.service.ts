import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private apiUrl = 'https://localhost:7003/api'; 

  constructor(private http: HttpClient) { }
 //  method to perform HTTP POST request
 post<T>(endpoint: string, data: any): Observable<T> {
  return this.http.post<T>(`${this.apiUrl}/${endpoint}`, data);
}
  
//  method to perform HTTP GET request
  get<T>(endpoint: string): Observable<T> {
    return this.http.get<T>(`${this.apiUrl}/${endpoint}`);
  }

 

//   //  method to perform HTTP PUT request
//   put<T>(endpoint: string, data: any): Observable<T> {
//     return this.http.put<T>(`${this.apiUrl}/${endpoint}`, data);
//   }

//   //  method to perform HTTP DELETE request
//   delete<T>(endpoint: string): Observable<T> {
//     return this.http.delete<T>(`${this.apiUrl}/${endpoint}`);
//   }
}
