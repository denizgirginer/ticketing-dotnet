import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

//import { MatToolbarModule } from '@angular/material/toolbar';
import { MaterialModule } from './material.module';
import { AuthModule } from './auth/auth.module';
import { TicketsModule } from './tickets/tickets.module';
import { MyordersModule } from './myorders/myorders.module';
import { SellTicketModule } from './sell-ticket/sell-ticket.module';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    //MatToolbarModule
    MaterialModule,
    AuthModule,
    TicketsModule,
    MyordersModule,
    SellTicketModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
