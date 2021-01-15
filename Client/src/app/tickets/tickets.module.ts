import { NgModule } from '@angular/core';
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
        path: ''
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
        MaterialModule,
        [RouterModule.forRoot(routes)]
    ],
    exports: []
})
export class TicketsModule {

}