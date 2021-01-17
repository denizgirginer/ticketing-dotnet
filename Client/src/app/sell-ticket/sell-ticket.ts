import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

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

    constructor(private api: ApiService) {

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

            if (res.id) {
                
            } else {
                this.error = "Error Sell Ticket.!";
            }
        } catch (e) {
            this.error = "Error Sell Ticket..!"
        }


    }
}