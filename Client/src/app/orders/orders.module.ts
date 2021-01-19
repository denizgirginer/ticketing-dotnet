import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { Routes, RouterModule } from '@angular/router'; // CLI imports router
import { MaterialModule } from '../material.module';

import { MyordersComponent } from './myorders';
import { ShowOrder } from './show-order';

const routes: Routes = [
    {
        component: MyordersComponent,
        path: ''
    },
    {
        component: MyordersComponent,
        path: 'myorders'
    },
    {
        component: ShowOrder,
        path: 'orders/:id'
    }
];

@NgModule({
    declarations:[
        MyordersComponent,
        ShowOrder
    ],
    imports: [
        BrowserModule,
        MaterialModule,
        [RouterModule.forRoot(routes)]
    ],
    exports: []
})
export class OrdersModule {

}