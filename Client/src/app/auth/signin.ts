import { Component, Input } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

import { AuthService } from '@services/auth.service';

@Component({
    selector: 'signin',
    templateUrl: './signin.html',
    styleUrls: ['./auth.css']
})
export class SigninComponent {
    form: FormGroup = new FormGroup({
        email: new FormControl(''),
        password: new FormControl(''),
    });

    @Input() error: string | null;

    constructor(private api: AuthService) {

    }

    async submit() {

        this.error = "";

        if (!this.form.valid) {
            this.error = "Enter valid input";
            return;
        }

        try {
            var res = await this.api.signin(this.form.value);

            if (res.success) {
                this.error = "Not Authorized.!";
            }
        } catch (e) {
            this.error = "Not Authorized.!"
        }


    }
}