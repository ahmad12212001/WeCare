import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FeedBackComponent } from './feed-back.component';
import { SharedModule } from '@shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule
  ],
  declarations: [FeedBackComponent],
  exports: [FeedBackComponent]
})
export class FeedBackModule { }
