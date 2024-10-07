import { Routes } from '@angular/router';
import { AuthComponent } from './auth/auth.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AuthGuard } from './services/AuthGuard/auth.guard';

export const routes: Routes = [

{
    path:'',
    component: AuthComponent
},

{ path: 'home', component: DashboardComponent, canActivate: [AuthGuard] },


];
