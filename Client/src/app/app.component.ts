import { Component } from '@angular/core';

import { AuthService } from '@services/auth.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Client';

  isLogin:boolean=false;

  signInSubs:Subscription;
  signOutSubs:Subscription;
  constructor(private authService:AuthService){

  }

  hasRole(role:string){
    return this.authService.hasRole(role);
  }
  
  ngOnInit(){
    this.isLogin = this.authService.isLogin();
    
    this.signInSubs = this.authService.$onSignin.subscribe(res=>{
      this.isLogin = res.success;
    })

    this.signOutSubs = this.authService.$onSignout.subscribe(res=>{
      this.isLogin = !res.success;
    })
  }

  ngOnDestroy(){
    this.signInSubs.unsubscribe();
  }
}
