import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { Routes, RouterModule } from '@angular/router'; // CLI imports router
import { MaterialModule } from '../material.module';

import { SellTicketComponent } from './sell-ticket';

const routes: Routes = [
    {
        component: SellTicketComponent,
        path: 'sell-ticket'
    }
];

@NgModule({
    declarations:[
        SellTicketComponent
    ],
    imports: [
        BrowserModule,
        ReactiveFormsModule,
        MaterialModule,
        [RouterModule.forRoot(routes)]
    ],
    exports: []
})
export class SellTicketModule {

}