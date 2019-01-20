import { Injectable, Inject } from '@angular/core';

import { Observable } from 'rxjs/Observable';
import { HttpClient } from '@angular/common/http';

import 'rxjs/add/operator/map';  
import 'rxjs/add/operator/catch';  
import 'rxjs/add/observable/throw';
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
      return this._http.get(this.appUrl + "api/Reminders/GetReminders/" + id)  
          .map((response: Response) => response.json())  
          .catch(this.errorHandler); 
  }  

  saveReminder(reminder : Reminder) : Observable<Reminder> {  
      return this._http.post(this.appUrl + 'api/Reminders/PostReminder', reminder)  
          .map((response: Response) => response.json())  
          .catch(this.errorHandler)  
  }  

  updateReminder(reminder : Reminder)  : Observable<Reminder>{  
      return this._http.put(this.appUrl + 'api/Reminders/PutReminder', reminder)  
          .map((response: Response) => response.json())  
          .catch(this.errorHandler);  
  }  

  deleteReminder(id : string) : Observable<Reminder> {  
      return this._http.delete(this.appUrl + "api/Reminders/DeleteReminder/" + id)  
          .map((response: Response) => response.json())  
          .catch(this.errorHandler);  
  }  

  errorHandler(error: Response) {  
      console.log(error);  
      return Observable.throw(error);  
  }  
}  
