import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ReminderComponent } from './reminder/reminder.component';
import { ReminderService } from './core/services/reminder.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    ReminderComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: ReminderComponent, pathMatch: 'full' },
    ])
  ],
  providers: [ReminderService],
  bootstrap: [AppComponent]
})
export class AppModule { }
