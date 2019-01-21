import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

import { NgbActiveModal, NgbDate } from '@ng-bootstrap/ng-bootstrap';
import { Reminder } from '../core/models/Reninder';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css']
})
export class ModalComponent implements OnInit {

  @Input() title : string;
  @Input() reminder : Reminder;
  @Output() passReminder : EventEmitter<Reminder> = new EventEmitter();

  reminderForm =  this.fb.group({
    subject: ['',  Validators.required],
    notes:  [''],
    date:  ['', Validators.required],
    time :  ['',  Validators.required],
    isActive:  [''],
  });

  constructor(
    public activeModal: NgbActiveModal,
    private fb: FormBuilder
    ) { }

  ngOnInit() {
    if(this.reminder){
      var date =  new Date( this.reminder.remindDate);
      this.reminderForm.patchValue(this.reminder);
      this.reminderForm.controls.time.setValue({hour : new Date( this.reminder.remindDate).getHours(), minute: new Date( this.reminder.remindDate).getMinutes()});
      this.reminderForm.controls.date.setValue(
        {
          year : new Date( this.reminder.remindDate).getFullYear(),
          month: new Date( this.reminder.remindDate).getMonth() + 1,
          day : new Date( this.reminder.remindDate).getDate()
        });
    }
    else{
      this.reminderForm.controls.date.setValue({year: new Date().getFullYear(), month: new Date().getMonth() + 1, day: new Date().getDate()});
    }
  }

  createReminder() : void{
    this.passReminder.emit(this.reminderForm.value);
  }

  update() : void{
    this.passReminder.emit(this.reminderForm.value);
  }

}
