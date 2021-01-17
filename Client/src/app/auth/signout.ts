import { Component } from '@angular/core';
import { AuthService } from '@services/auth.service';


@Component({
    selector:'signout',
    templateUrl:'./signout.html',
    styleUrls:['./auth.css']
}) 
export class SignoutComponent { 

    constructor(private authService:AuthService){

    }    

    ngOnInit(){
        setTimeout(()=>{
            this.signout();
        },1000)
    }


    async signout(){
        try {
            await this.authService.signout();
           
        } catch(e) {

        }
    }
}