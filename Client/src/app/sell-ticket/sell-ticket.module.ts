import { NgModule } from '@angular/core';
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
        MaterialModule,
        [RouterModule.forRoot(routes)]
    ],
    exports: []
})
export class SellTicketModule {

}