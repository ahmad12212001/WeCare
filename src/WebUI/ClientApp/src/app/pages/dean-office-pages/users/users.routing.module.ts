
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UsersComponent } from './users.component';
import { UserOperationComponent } from './user-operation/user-operation.component';
import { ChangePasswordComponent } from './change-password/change-password.component';

const routes: Routes = [
    {
        path: '',
        component: UsersComponent,
        data: {
            title: 'Users'
        },
    },
    {
        path: 'operation',
        component: UserOperationComponent,
        data: {
            title: 'Users'
        },
    },
    {
        path: 'operation/:id',
        component: UserOperationComponent,
        data: {
            title: 'Users'
        },
    },
    {
        path: 'change-password/:id',
        component: ChangePasswordComponent,
        data: {
            title: 'Users'
        },
    }


];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class UsersRoutingModule { }
