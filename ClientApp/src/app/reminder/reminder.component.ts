import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Reminder } from '../core/models/Reninder';
import { ReminderService } from '../core/services/reminder.service';
import { Observable } from 'rxjs';
import { of } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalComponent } from '../modal/modal.component';
import { DatePipe } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'reminder',
  templateUrl: './reminder.component.html',
  styleUrls: ['./reminder.component.css']
})
export class ReminderComponent implements OnInit {
  reminders$: Observable< Reminder[]>;
  currentDate : string;


  constructor(
    private service : ReminderService,
    private modalService: NgbModal,
    private datePipe: DatePipe,
    private toastr: ToastrService
    ){
  }

  ngOnInit(): void {
    this.reminders$ = this.service.getReminders();
    this.currentDate = this.datePipe.transform(new Date(), 'yyyy-MM-dd');
  }

  addNew(reminder : Reminder){
    const modalRef = this.modalService.open(ModalComponent);
    modalRef.componentInstance.title = "Create";
    modalRef.componentInstance.passReminder.subscribe( (receivedEntry) => {

      receivedEntry.remindDate = `${receivedEntry.date.year}-${receivedEntry.date.month}-${receivedEntry.date.day} 
        ${receivedEntry.time.hour}:${receivedEntry.time.minute}`;

      this.service.saveReminder(receivedEntry).subscribe(resp => {
        if(resp){
          this.toastr.success('Remainder created');
          this.service.getReminders().subscribe((resp) => {
            this.reminders$ = of(resp);
          });
          modalRef.close();
        }        
      });
    });
  }

  update(reminder : Reminder) : void{
    const modalRef = this.modalService.open(ModalComponent);
    modalRef.componentInstance.title = "Update";
    modalRef.componentInstance.reminder = reminder;

    modalRef.componentInstance.passReminder.subscribe( (receivedEntry) => {

      receivedEntry.remindDate = `${receivedEntry.date.year}-${receivedEntry.date.month}-${receivedEntry.date.day} 
        ${receivedEntry.time.hour}:${receivedEntry.time.minute}`;

      this.service.updateReminder(receivedEntry).subscribe(resp => {
        if(resp){
          this.toastr.success('Remainder updated');
          this.service.getReminders().subscribe((resp) => {
            this.reminders$ = of(resp);
          });
          modalRef.close();
        }        
      });
    });
  }

  delete(id : string) : boolean{
    this.service.deleteReminder(id)
     .subscribe(resp => {
       if(resp.id == id){
        this.service.getReminders().subscribe((resp) => {
          this.reminders$ = of(resp);
        });
        this.toastr.success('Remainder deleted');
        return true;
       }
       return false;
     });

     return false;
  }

  activate(id : string){
    this.service.activateReminder(id).subscribe(() => {
      this.service.getReminders().subscribe((resp) => {
        this.toastr.success('Remainder activated');
        this.reminders$ = of(resp);
      });
    });
  }

  deactivate(id : string){
    this.service.deactivateReminder(id).subscribe(() =>{
      this.service.getReminders().subscribe((resp) => {
        this.toastr.success('Remainder deactivated');
        this.reminders$ = of(resp);
      });
    });
  }
}
