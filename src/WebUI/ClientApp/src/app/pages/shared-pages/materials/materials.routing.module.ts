
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MaterialsComponent } from './materials.component';
import { MaterialOperationComponent } from './material-operation/material-operation.component';




const routes: Routes = [
    {
        path: '',
        component: MaterialsComponent,
        data: {
            title: 'Materials'
        },
    },
    {
        path: 'operation',
        component: MaterialOperationComponent,
        data: {
            title: 'Materials'
        },
    },
    {
        path: 'operation/:requestId',
        component: MaterialOperationComponent,
        data: {
            title: 'Materials'
        },
    }

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class MaterialsRoutingModule { }
