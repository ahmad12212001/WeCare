
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { StudentsComponent } from './students.component';
import { StudentOperationComponent } from './student-operation/student-operation.component';
import { StudentCourseOperationComponent } from './student-course-operation/student-course-operation.component';




const routes: Routes = [
    {
        path: '',
        component: StudentsComponent,
        data: {
            title: 'Students'
        },
    },
    {
        path: 'operation',
        component: StudentOperationComponent,
        data: {
            title: 'Students'
        },
    },
    {
        path: 'operation/:id',
        component: StudentOperationComponent,
        data: {
            title: 'Students'
        },
    },
    {
        path: 'operation-courses/:id',
        component: StudentCourseOperationComponent,
        data: {
            title: 'Students'
        },
    },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class StudentsRoutingModule { }
