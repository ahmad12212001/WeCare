import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LandingPageComponent } from './landing-page.component';
import { LandingPageRoutingModule } from './landing-page.routing.module';

@NgModule({
  imports: [
    CommonModule,
    LandingPageRoutingModule
  ],
  declarations: [LandingPageComponent]
})
export class LandingPageModule { }
