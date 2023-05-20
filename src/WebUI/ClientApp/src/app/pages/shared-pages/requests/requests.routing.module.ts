
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RequestsComponent } from './requests.component';
import { RequestOperationComponent } from './request-operation/request-operation.component';



const routes: Routes = [
    {
        path: '',
        component: RequestsComponent,
        data: {
            title: 'Requests'
        },
    },
    {
        path: 'operation',
        component: RequestOperationComponent,
        data: {
            title: 'Requests'
        },
    }

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class RequestsRoutingModule { }
