import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

import { ApiService } from '@services/api.service';
import { AuthService } from '@services/auth.service';

interface TicketsResult {
    title: string;
    price: Number;
    id: string;
    createdAt: Date;
    version: Number;
};

@Component({
    selector: 'tickets',
    templateUrl: './tickets.html',
    styleUrls: ['./tickets.css']
})
export class TicketsComponent {

    tickets: TicketsResult[] = [];
    isLogin: boolean = false;
    constructor(
        private api: ApiService, 
        private authService: AuthService,
        private router:Router,
        private _snackBar: MatSnackBar
    ) {

    }

    openSnackBar(message: string, action: string) {
        this._snackBar.open(message, action, {
          duration: 2000,
        });
      }

    ngOnInit() {
        this.isLogin = this.authService.isLogin();
        this.api.get<TicketsResult[]>("/tickets").subscribe(res => {
            this.tickets = res;
        })
    }

    PurchaseTicket(ticket: TicketsResult) {
        this.api.post<any>("/orders", {
            ticketId:ticket.id
        }).subscribe(order=>{
            if(order.success) {
                this.router.navigateByUrl(`/orders/${order.id}`);
            }
        })
    }

    DeleteTicket(ticket: TicketsResult) {
        this.api.delete<any>(`/tickets/${ticket.id}`).subscribe(res => {
            if (res.success == true) {
                let idx = this.tickets.indexOf(ticket);

                this.tickets.splice(idx, 1);
            }
        }, (error)=>{
            if(error.status == 403) {
                this.openSnackBar('Not Authorized for delete', 'Dismiss')
            }
        })
    }
}