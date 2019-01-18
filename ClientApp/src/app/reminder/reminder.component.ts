import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'reminder',
  templateUrl: './reminder.component.html',
  styleUrls: ['./reminder.component.css']
})
export class ReminderComponent {
  public reminders: Reminder[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Reminder[]>(baseUrl + 'api/Reminders/GetReminders').subscribe(result => {
      this.reminders = result;
    }, error => console.error(error));
  }
}

interface Reminder {
  id: string,
  subject: string,
  notes: string,
  remindDate: Date,
  isActive: boolean,
  isCancelled: boolean
}
