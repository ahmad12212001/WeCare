import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthorizeGuard } from '../api-authorization/authorize.guard';
import { ErrorStyleComponent } from './shared/components/layouts/error-style/error-style.component';
import { error_content } from './shared/routes/error-content-router';
import { FullContentComponent } from './shared/components/layouts/full-content/full-content.component';


export const routes: Routes = [
  { path: '', redirectTo: '/authentication/login', pathMatch: 'full' },
  {
    path: '', component: FullContentComponent, children: [
      {
        path: 'exams',
        canActivate: [AuthorizeGuard],
        loadChildren: () => import('../app/pages/academic-staff-pages/exams/exams.module').then(m => m.ExamsModule)
      }
    ]
  }
  ,
  { path: '', component: ErrorStyleComponent, children: error_content },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
