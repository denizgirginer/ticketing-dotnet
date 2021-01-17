import { Component } from '@angular/core';

import { ApiService } from '@services/api.service';

@Component({
    selector:'myorders',
    templateUrl:'./myorders.html'
}) 
export class MyordersComponent { 
    
    constructor(private api:ApiService) {

    }

    ngOnInit(){

        this.api.get("/orders").subscribe(res=>{
            console.log(res);
        })
    }
}