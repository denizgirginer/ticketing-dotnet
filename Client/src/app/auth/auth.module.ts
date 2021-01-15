import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router'; // CLI imports router
import { MaterialModule } from '../material.module';

import { SigninComponent } from './signin';
import { SignupComponent } from './signup';
import { SignoutComponent } from './signout';

const routes: Routes = [
    {
        component: SigninComponent,
        path: 'auth/signin'
    },
    {
        component: SignupComponent,
        path: 'auth/signup'
    },
    {
        component: SignoutComponent,
        path: 'auth/signout'
    }
];

@NgModule({
    declarations:[
        SigninComponent,
        SignupComponent,
        SignoutComponent
    ],
    imports: [
        MaterialModule,
        [RouterModule.forRoot(routes)]
    ],
    exports: []
})
export class AuthModule {

}