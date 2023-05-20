import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SafePipe } from './safe.pipe';
import { ContentTypePipe } from './content-type.pipe';



@NgModule({
    imports: [CommonModule],
    declarations: [SafePipe, ContentTypePipe],
    providers: [],
    exports: [SafePipe, ContentTypePipe]
})
export class PipesModule { }
