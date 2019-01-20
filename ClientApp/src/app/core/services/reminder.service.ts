import { Injectable, Inject } from '@angular/core';

import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

import {from} from 'rxjs';  
import {map} from 'rxjs/operators'
import { Reminder } from '../models/Reninder';

@Injectable()
export class ReminderService {

  appUrl: string = "";  
  
  constructor(private _http: HttpClient, @Inject('BASE_URL') baseUrl: string) {  
      this.appUrl = baseUrl;  
  }  

  getReminders() : Observable<Reminder[]> { 
      return this._http.get<Reminder[]>(this.appUrl + 'api/Reminders/GetReminders');
  }  

  getReminderById(id: number) : Observable<Reminder>{  
      return this._http.get<Reminder>(this.appUrl + "api/Reminders/GetReminders/" + id);
  }  

  saveReminder(reminder : Reminder) : Observable<Reminder> {  
      return this._http.post<Reminder>(this.appUrl + 'api/Reminders/PostReminder', reminder);  
  }  

  updateReminder(reminder : Reminder)  : Observable<Reminder>{  
      return this._http.put<Reminder>(this.appUrl + 'api/Reminders/PutReminder', reminder); 
  }  

  deleteReminder(id : string) : Observable<Reminder> {  
      return this._http.delete<Reminder>(this.appUrl + "api/Reminders/DeleteReminder/" + id); 
  }  

  errorHandler(error: Response) {  
      console.log(error);  
      return Observable.throw(error);  
  }  
}  
