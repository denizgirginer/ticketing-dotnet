import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormControl, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

import { ApiService } from '@services/api.service';

@Component({
    selector:'ticket-view',
    templateUrl:'./ticket-view.html',
    styleUrls:['./ticket-view.css']
})
export class TicketView {
    ticketId:string;

    form: FormGroup = new FormGroup({
        title: new FormControl(''),
        price: new FormControl(''),
    });

    error: string | null;

    constructor(private api: ApiService, private _snackBar: MatSnackBar, activeRoute:ActivatedRoute) {

        let id = activeRoute.snapshot.paramMap.get("id");

        this.ticketId =  id;
    }

    async ngOnInit(){
        console.log(this.ticketId);
        let ticket = await this.api.getAsync<any>("/tickets/"+this.ticketId);

        console.log(ticket);

        
        this.form.setValue({
            title:ticket.title,
            price:ticket.price
        })
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
                id:this.ticketId,
                ...this.form.value
            }

            data.price = parseInt(data.price);

            var res = await this.api.putAsync<any>("/tickets/"+this.ticketId, data);

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