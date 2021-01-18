import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { Routes, RouterModule } from '@angular/router'; // CLI imports router
import { MaterialModule } from '../material.module';

import { TicketsComponent } from './tickets';
import { TicketView } from './ticket-view';

const routes: Routes = [
    // {
    //     path: '**',
    //     component: TicketsComponent
    // },
    {
        component: TicketsComponent,
        path: '',
        pathMatch: 'full'
    },
    {
        component: TicketsComponent,
        path: 'tickets'
    },
    {
        component: TicketView,
        path: 'tickets/:id'
    }
];

@NgModule({
    declarations:[
        TicketsComponent,
        TicketView
    ],
    imports: [
        BrowserModule,
        ReactiveFormsModule,
        MaterialModule,
        [RouterModule.forRoot(routes)]
    ],
    exports: []
})
export class TicketsModule {

}