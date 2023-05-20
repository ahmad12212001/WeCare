import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthorizeGuard } from '../api-authorization/authorize.guard';
import { ErrorStyleComponent } from './shared/components/layouts/error-style/error-style.component';
import { error_content } from './shared/routes/error-content-router';
import { FullContentComponent } from './shared/components/layouts/full-content/full-content.component';



export const routes: Routes = [
  {
    path: '', component: FullContentComponent, canActivate: [AuthorizeGuard], children: [
      {
        path: 'exams',
        loadChildren: () => import('../app/pages/academic-staff-pages/exams/exams.module').then(m => m.ExamsModule)
      },
      {
        path: 'courses',
        loadChildren: () => import('../app/pages/academic-staff-pages/courses/courses.module').then(m => m.CoursesModule)
      },
      {
        path: 'requests',
        loadChildren: () => import('../app/pages/shared-pages/requests/requests.module').then(m => m.RequestsModule)
      },
      {
        path: 'materials',
        loadChildren: () => import('../app/pages/shared-pages/materials/materials.module').then(m => m.MaterialsModule)
      },
      { path: '', redirectTo: 'exams', pathMatch: 'full' }
    ]
  }
  ,
  { path: '', component: ErrorStyleComponent, children: error_content }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
