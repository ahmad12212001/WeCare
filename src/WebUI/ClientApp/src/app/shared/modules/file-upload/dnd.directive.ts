import { Directive, HostListener, HostBinding, EventEmitter, Output, Input } from '@angular/core';

@Directive({
  selector: '[dnd]'
})
export class DndDirective {
  @Input() public allowed_extensions: Array<string> = [];
  @Output() private filesChangeEmiter: EventEmitter<File[]> = new EventEmitter();
  @Output() private filesInvalidEmiter: EventEmitter<File[]> = new EventEmitter();
  @Output() private originalContent: EventEmitter<any> = new EventEmitter();
  @HostBinding('style.background') public background!: any;

  constructor() { }

  @HostListener('dragover', ['$event']) public onDragOver(evt: any) {
    evt.preventDefault();
    evt.stopPropagation();
    this.background = '#999';
  }

  @HostListener('dragleave', ['$event']) public onDragLeave(evt: any) {
    evt.preventDefault();
    evt.stopPropagation();
    this.background = '#eee';
  }

  @HostListener('drop', ['$event']) public onDrop(evt: any) {
    evt.preventDefault();
    evt.stopPropagation();
    this.background = '#fff';
    const files = evt.dataTransfer.files;
    const valid_files: Array<File> = [];
    const invalid_files: Array<File> = [];
    if (files.length > 0) {
      for (let index = 0; index < files.length; index++) {
        const ext = files[index].name.split('.')[files[index].name.split('.').length - 1];
        if (this.allowed_extensions.lastIndexOf(ext) !== -1) {
          valid_files.push(files[index]);
        } else {
          invalid_files.push(files[index]);
        }
      }
      this.originalContent.emit(evt);
      this.filesChangeEmiter.emit(valid_files);
      this.filesInvalidEmiter.emit(invalid_files);
    }
  }
}
