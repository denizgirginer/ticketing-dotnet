import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { Routes, RouterModule } from '@angular/router'; // CLI imports router
import { MaterialModule } from '../material.module';

import { TicketsComponent } from './tickets';

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
    }
];

@NgModule({
    declarations:[
        TicketsComponent
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