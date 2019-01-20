import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Reminder } from '../core/models/Reninder';
import { ReminderService } from '../core/services/reminder.service';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'reminder',
  templateUrl: './reminder.component.html',
  styleUrls: ['./reminder.component.css']
})
export class ReminderComponent {
   reminders$: Observable< Reminder[]>;

  constructor(private service : ReminderService){
    this.reminders$ = service.getReminders();
  }

  update(reminderId : string ) : void{
    alert(reminderId);
  }

  delete(id : string) : boolean{
    this.service.deleteReminder(id)
     .subscribe(resp => {
       if(resp.id == id){
         return true;
       }
       return false;
     });

     return false;
  }
}
