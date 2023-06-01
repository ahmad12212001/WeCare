
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MajorsComponent } from './majors.component';
import { MajorsOperationComponent } from './majors-operation/majors-operation.component';




const routes: Routes = [
    {
        path: '',
        component: MajorsComponent,
        data: {
            title: 'Majors'
        },
    },
    {
        path: 'operation',
        component: MajorsOperationComponent,
        data: {
            title: 'Majors'
        },
    },
    {
        path: 'operation/:id',
        component: MajorsOperationComponent,
        data: {
            title: 'Majors'
        },
    },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class MajorsRoutingModule { }
