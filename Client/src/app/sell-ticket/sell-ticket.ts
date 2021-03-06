import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

import { ApiService } from '@services/api.service';

@Component({
    selector:'sell-ticket',
    templateUrl:'./sell-ticket.html',
    styleUrls:['./sell-ticket.css']
}) 
export class SellTicketComponent { 
    form: FormGroup = new FormGroup({
        title: new FormControl(''),
        price: new FormControl(''),
    });

    error: string | null;

    constructor(private api: ApiService, private _snackBar: MatSnackBar) {

    }

    openSnackBar(message: string, action: string) {
        this._snackBar.open(message, action, {
            duration: 2000,
        });
    }

    async submit() {

        this.error = "";

        if (!this.form.valid) {
            this.error = "Enter valid input";
            return;
        }

        try {
            let data = {
                ...this.form.value
            }

            data.price = parseInt(data.price);

            var res = await this.api.postAsync<any>("/tickets", data);

            this.form.reset();

            if (res.id) {
                this.openSnackBar("Ticket saved succesfully..", "Dismiss")
            } else {
                this.error = "Error Sell Ticket.!";
            }
        } catch (e) {
            this.error = "Error Sell Ticket..!"
        }


    }
}