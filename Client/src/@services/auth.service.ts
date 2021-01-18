import { Injectable, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';

import { ApiService } from '@services/api.service';

interface SignInResult {
    success?: boolean;
    token?: string;
    error: string;
}

interface SignOutResult {
    success?: boolean;
    error?: string;
}

@Injectable()
export class AuthService {
    public $onSignin = new EventEmitter<SignInResult>();
    public $onSignout = new EventEmitter<SignOutResult>();
    constructor(private api: ApiService, private router: Router) {

    }

    isLogin() {
        return !this.tokenExpired();
    }

    private getTokenObject(){
        let tokenStr = localStorage.getItem("auth-jwt");
        if (!tokenStr)
            return false;
        let token = JSON.parse(atob(tokenStr.split(".")[1]));

        return token;
    }

    hasRole(role: string) {
        if (this.tokenExpired())
            return false;
            
        let token = this.getTokenObject();

        if(token==false)
            return false;

        let claim = token[role];

        return !!claim;
    }

    tokenExpired() {
        const token = this.getTokenObject();
        if(token==false)
            return true;

        const expiry = token.exp;
        return (Math.floor((new Date).getTime() / 1000)) >= expiry;
    }

    async signin(data: any) {
        try {
            let res = await this.api.postAsync<SignInResult>("/users/signin", data);

            if (res.success) {
                localStorage.setItem("auth-jwt", res.token);
                this.$onSignin.emit(res);
                this.router.navigateByUrl("/")
            }

            return res;
        } catch (e) {
            let res: SignInResult = {
                error: 'Signin Error'
            }

            return res;
        }
    }

    async signout() {
        try {
            let res = await this.api.postAsync<SignOutResult>("/users/signout");

            if (res.success) {
                localStorage.removeItem("auth-jwt");
                this.$onSignout.emit(res);
                this.router.navigateByUrl("/")
            }

            return res;
        } catch (e) {
            let res: SignOutResult = {
                error: 'Signin Error'
            }

            return res;
        }
    }
}