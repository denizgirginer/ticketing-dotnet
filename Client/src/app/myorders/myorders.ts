import { Component } from '@angular/core';

import { ApiService } from '@services/api.service';

@Component({
    selector:'myorders',
    templateUrl:'./myorders.html'
}) 
export class MyordersComponent { 
    
    orders:any[]=[];

    constructor(private api:ApiService) {

    }

    ngOnInit(){

        this.api.get<any[]>("/orders").subscribe(res=>{
            this.orders = res;
        })
    }
}