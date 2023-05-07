
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ExamsComponent } from './exams.component';
import { ExamOperationComponent } from './exam-operation/exam-operation.component';



const routes: Routes = [
    {
        path: '',
        component: ExamsComponent,
        data: {
            title: 'Exams'
        },
    },
    {
        path: 'operation',
        component: ExamOperationComponent,
        data: {
            title: 'Exams'
        },
    },
    {
        path: 'operation/:id',
        component: ExamOperationComponent,
        data: {
            title: 'Exams'
        },
    },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class ExamsRoutingModule { }
