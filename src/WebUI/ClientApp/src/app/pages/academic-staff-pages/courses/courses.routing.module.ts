
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CoursesComponent } from './courses.component';
import { CourseOperationComponent } from './course-operation/course-operation.component';



const routes: Routes = [
    {
        path: '',
        component: CoursesComponent,
        data: {
            title: 'Course'
        },
    },
    {
        path: 'operation',
        component: CourseOperationComponent,
        data: {
            title: 'Course'
        },
    },
    {
        path: 'operation/:id',
        component: CourseOperationComponent,
        data: {
            title: 'Course'
        },
    },

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class CoursesRoutingModule { }
