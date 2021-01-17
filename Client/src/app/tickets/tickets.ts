import { Component } from '@angular/core';

import { ApiService } from '@services/api.service';
import { AuthService } from '@services/auth.service';

interface TicketsResult {
    title:string;
    price:Number;
    id:string;
    createdAt:Date;
    version:Number;
};

@Component({
    selector:'tickets',
    templateUrl:'./tickets.html',
    styleUrls:['./tickets.css']
}) 
export class TicketsComponent { 

    tickets:TicketsResult[]=[];
    isLogin:boolean=false;
    constructor(private api:ApiService, private authService:AuthService) {

    }

    ngOnInit(){
        this.isLogin = this.authService.isLogin();
        this.api.get<TicketsResult[]>("/tickets").subscribe(res=>{
            this.tickets = res;
        })
    }
}