import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable()
export class ApiService {
    
    baseUrl:string = "http://ticketing.com/api";

    constructor(private http:HttpClient) {

    }

    getHeaders(){
        let auth_token = localStorage.getItem("auth-jwt");

        if(!!auth_token)
        return  new HttpHeaders({
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${auth_token}`
          })

        return null;
    }

    post<T>(url:string, data?:any, opts?:any) {
        let headers = this.getHeaders();

        return this.http.post<T>(this.baseUrl+url, data, {headers:headers});
    }

    postAsync<T>(url:string, data?:any, opts?:any) {
        let headers = this.getHeaders();

        return this.http.post<T>(this.baseUrl+url, data, {headers:headers}).toPromise();
    }

    get<T>(url:string, opts?:any) {
        let headers = this.getHeaders();

        return this.http.get<T>(this.baseUrl+url, {headers:headers});
    }

    getAsync<T>(url:string, opts?:any) {
        let headers = this.getHeaders();

        return this.http.get<T>(this.baseUrl+url, {headers:headers}).toPromise();
    }
}