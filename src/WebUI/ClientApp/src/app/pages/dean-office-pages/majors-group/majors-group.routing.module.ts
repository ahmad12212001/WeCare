
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MajorsGroupComponent } from './majors-group.component';
import { MajorsGroupOperationComponent } from './majors-group-operation/majors-group-operation.component';




const routes: Routes = [
    {
        path: '',
        component: MajorsGroupComponent,
        data: {
            title: 'Major Groups'
        },
    },
    {
        path: 'operation',
        component: MajorsGroupOperationComponent,
        data: {
            title: 'Major Groups'
        },
    },
    {
        path: 'operation/:id',
        component: MajorsGroupOperationComponent,
        data: {
            title: 'Major Groups'
        },
    },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class MajorGroupsRoutingModule { }
