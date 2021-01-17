import { Component, Input } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ApiService } from '@services/api.service';

interface SignupResult {
    email:string;
    id:string;
}

@Component({
    selector:'signup',
    templateUrl:'./signup.html',
    styleUrls:["./auth.css"]
}) 
export class SignupComponent { 
    form: FormGroup = new FormGroup({
        email: new FormControl(''),
        password: new FormControl(''),
    });

    @Input() error: string | null;

    constructor(private api: ApiService) {

    }

    async submit() {

        this.error = "";

        if (!this.form.valid) {
            this.error = "Enter valid input";
            return;
        }

        try {
            var res = await this.api.postAsync<SignupResult>("/users/signup", this.form.value);

            if (res.id) {
                
            } else {
                this.error = "Error Signup.!";
            }
        } catch (e) {
            this.error = "Error Signup..!"
        }


    }
}