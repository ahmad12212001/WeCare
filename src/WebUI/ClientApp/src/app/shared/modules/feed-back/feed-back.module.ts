import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FeedBackComponent } from './feed-back.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [FeedBackComponent],
  exports: [FeedBackComponent]
})
export class FeedBackModule { }
