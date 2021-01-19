import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ApiService } from '@services/api.service';

@Component({
    selector:'show-order',
    templateUrl:'./show-order.html',
    styleUrls:['./show-order.css']
})
export class ShowOrder {

    orderId:string;

    order:any;

    timeLeft:number;
    timerId:any;

    nowDate:any;
    constructor(
        private api:ApiService,
        private route:ActivatedRoute
    ) {

        let id = route.snapshot.paramMap.get("id");

        this.orderId = id;
    }

    ngOnInit(){
        this.api.get<any>("/orders/"+this.orderId).subscribe(res=>{
            this.order = res;

            console.log(res);

            this.InitExpireTimer();
        })
    }

    ngOnDestroy(){
        clearInterval(this.timerId);
    }

    InitExpireTimer(){
        const findTimeLeft= ()=>{
            let dt1 = new Date(this.order.expiresAt);
            let dt2 = new Date();

            this.nowDate = new Date();

            let msLeft = dt1.getTime()-dt2.getTime();
            

            this.timeLeft = Math.round(msLeft/1000)
        }

        findTimeLeft();

        this.timerId=setInterval(findTimeLeft, 1000)
    }
}