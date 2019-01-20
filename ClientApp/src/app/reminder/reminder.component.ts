import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Reminder } from '../core/models/Reninder';
import { ReminderService } from '../core/services/reminder.service';
import { Observable } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalComponent } from '../modal/modal.component';

@Component({
  selector: 'reminder',
  templateUrl: './reminder.component.html',
  styleUrls: ['./reminder.component.css']
})
export class ReminderComponent {
   reminders$: Observable< Reminder[]>;

  constructor(
    private service : ReminderService,
    private modalService: NgbModal
    ){
    this.reminders$ = service.getReminders();
  }

  update(reminderId : string ) : void{
    const modalRef = this.modalService.open(ModalComponent);
    modalRef.componentInstance.name = 'World';
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
