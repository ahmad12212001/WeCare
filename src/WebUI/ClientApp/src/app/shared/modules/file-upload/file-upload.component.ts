import { HttpClient, HttpHeaders, HttpEventType, HttpResponse } from '@angular/common/http';
import { Component, Input, OnChanges, OnInit, SimpleChanges, ViewEncapsulation, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Observable, Subject } from 'rxjs';
import { FileUploadOptions } from './file-upload-options';

@Component({
  selector: 'file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => FileUploadComponent),
      multi: true
    }
  ]
})
export class FileUploadComponent implements ControlValueAccessor, OnChanges {

  onChange: any = () => { };
  onTouched: any = () => { };
  disabled = false;
  value!: File | string;
  @Input() submitted$!: Observable<boolean | null>;
  invalid$ = new Subject<boolean | null>();
  @Input() options: FileUploadOptions;
  extensionsWithDots: string[];
  name = 'Angular';
  @Input() progress$: Observable<{ percentage: string }>;

  constructor() { }

  ngOnChanges(): void {
    this.extensionsWithDots = [...this.options.allowedExtensions, ...allExtensions].map(e => '.' + e);
  }

  writeValue(value: any): void {
    if (value) {
      this.value = value;
    }
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  getNewFileExtention(file: File | any): string {
    if (file) {
      return file.name.toLowerCase().match(/\.[0-9a-z]+$/i)[0];
    } else {
      return '';
    }
  }

  fileEvent(e: any) {
    if (e) {
      let file = null;
      if (e && e.target) {
        file = e.target.files[0];
      } else {
        if (e.length > 0) {
          file = e[0];
        }
        else {
          this.invalid$.next(true);
        }
      }
      if (file) {
        const validatedFile = this.validateFileExtensions(file);
        if (validatedFile) {
          this.addFiles(validatedFile);
        }
      }
    } else {
      this.invalid$.next(true);
    }
  }

  validateFileExtensions(file: File): File | null {
    const validFiles = this.extensionsWithDots.includes(this.getNewFileExtention(file));
    if (!validFiles) {
      this.onFileInvalid();
      return null;
    } else {
      this.invalid$.next(null);
      return file;
    }
  }

  onFileInvalid(): void {
    this.invalid$.next(true);
  }
  addFiles(file: File): void {
    this.value = file;
    this.onChange(this.value);
  }

}


export const allExtensions = ['jpeg', 'jpg', 'png', 'gif', 'svg', 'pdf', 'doc', 'xdoc', 'mp3', 'wave', 'mp4'];