import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ReminderComponent } from './reminder/reminder.component';
import { ReminderService } from './core/services/reminder.service';
import { HttpClientModule } from '@angular/common/http';

import { MomentModule } from 'angular2-moment';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ModalComponent } from './modal/modal.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    ReminderComponent,
    ModalComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    MomentModule,
    NgbModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: ReminderComponent, pathMatch: 'full' },
    ])
  ],
  entryComponents : [
    ModalComponent
  ],
  providers: [ReminderService],
  bootstrap: [AppComponent]
})
export class AppModule { }
