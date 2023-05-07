import { Component, forwardRef, Input } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Option } from '@shared/models/option';

@Component({
  selector: 'drop-down',
  templateUrl: './drop-down.component.html',
  styleUrls: ['./drop-down.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => DropDownComponent),
      multi: true
    }
  ]
})
export class DropDownComponent implements ControlValueAccessor {

  @Input() options: Option[] = [];
  @Input() label!: string;
  @Input() errorMessage!: string;
  @Input() placeholder!: string;
  disabled = false;
  value!: Option;
  opened: boolean = false;

  onChange: any = () => { };
  onTouched: any = () => { };

  constructor() { }

  writeValue(value: Option): void {

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

  closeDll(event: any) {
    this.opened = false;
  }

  setItem(item: Option) {
    this.value = item;
    this.onChange(this.value);
    this.openDll();
  }
  openDll() {
    this.opened = !this.opened;
  }


}
