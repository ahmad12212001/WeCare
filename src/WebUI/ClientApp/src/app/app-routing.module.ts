import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthorizeGuard } from '../api-authorization/authorize.guard';
import { ErrorStyleComponent } from './shared/components/layouts/error-style/error-style.component';
import { error_content } from './shared/routes/error-content-router';
import { FullContentComponent } from './shared/components/layouts/full-content/full-content.component';



export const routes: Routes = [
  { path: '', loadChildren: () => import('../app/pages/home/home.module').then(m => m.HomeModule) },
  { path: 'home', loadChildren: () => import('../app/pages/home/home.module').then(m => m.HomeModule) },
  {
    path: '', component: FullContentComponent, canActivate: [AuthorizeGuard], children: [
      {
        path: 'exams',
        loadChildren: () => import('../app/pages/academic-staff-pages/exams/exams.module').then(m => m.ExamsModule)
      },
      {
        path: 'courses',
        loadChildren: () => import('../app/pages/dean-office-pages/courses/courses.module').then(m => m.CoursesModule)
      },
      {
        path: 'requests',
        loadChildren: () => import('../app/pages/shared-pages/requests/requests.module').then(m => m.RequestsModule)
      },
      {
        path: 'materials',
        loadChildren: () => import('../app/pages/shared-pages/materials/materials.module').then(m => m.MaterialsModule)
      },
      {
        path: 'majors',
        loadChildren: () => import('../app/pages/dean-office-pages/majors/majors.module').then(m => m.MajorsModule)
      },
      {
        path: 'major-groups',
        loadChildren: () => import('../app/pages/dean-office-pages/majors-group/majors-group.module').then(m => m.MajorsGroupModule)
      },
      {
        path: 'students',
        loadChildren: () => import('../app/pages/shared-pages/students/students.module').then(m => m.StudentsModule)
      },
      {
        path: 'users',
        loadChildren: () => import('../app/pages/dean-office-pages/users/users.module').then(m => m.UsersModule)
      },
      { path: 'landing-page', loadChildren: () => import('../app/pages/landing-page/landing-page.module').then(m => m.LandingPageModule) },
      { path: '', redirectTo: 'landing-page', pathMatch: 'full' }
    ]
  }
  ,

  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '', component: ErrorStyleComponent, children: error_content }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
