
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LandingPageComponent } from './landing-page.component';



const routes: Routes = [
    {
        path: '',
        component: LandingPageComponent,
        data: {
            title: 'Home'
        },
    }

];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class LandingPageRoutingModule { }
