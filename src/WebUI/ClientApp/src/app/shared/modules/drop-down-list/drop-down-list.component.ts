import { ChangeDetectorRef, Component, EventEmitter, forwardRef, Input, OnChanges, Output, ViewEncapsulation } from '@angular/core';
import { ControlValueAccessor, FormControl, NG_VALUE_ACCESSOR, Validators } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { Option } from '@shared/models/option';

@Component({
  selector: 'drop-down-list',
  templateUrl: './drop-down-list.component.html',
  styleUrls: ['./drop-down-list.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => DropDownListComponent),
      multi: true
    }
  ],

})
export class DropDownListComponent implements OnChanges, ControlValueAccessor {

  @Input() options: Option[] | any | {} = [];
  @Input() hasAddOption: boolean = false;
  filteredList$: Observable<Option[]> = of([]);
  disabled = false;
  value!: Option | null;
  opened: boolean = false;
  searchControl = new FormControl('');
  onChange: any = () => { };
  onTouched: any = () => { };
  @Input() placeholder!: string;
  @Input() id!: string;
  @Input() label!: string;
  @Input() isRequired: boolean = false;
  @Input() errorMessage: string = '';
  @Output() onAdd = new EventEmitter();
  @Output() change = new EventEmitter();
  constructor(private cdr: ChangeDetectorRef) { }
  ngOnChanges(): void {
    this.filteredList$ = this.searchControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value))
    );

  }
  writeValue(value: Option): void {
    if (value) {
      this.value = value;
    } else {
      this.value = null;
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
    this.cdr.detectChanges();
  }
  setItem(item: Option) {
    this.value = item;
    this.change.emit(this.value);
    this.onChange(this.value);
    this.openDll();
  }

  add() {
    this.onAdd.emit(true);
  }
  openDll() {

    this.opened = !this.opened;
    this.cdr.detectChanges();
  }
  private _filter(value: string): Option[] {
    const filterValue = value.toLowerCase();
    return this.options.filter((option: any) => option.name.toLowerCase().indexOf(filterValue) === 0);
  }
  onInput(event: any) {
    event.preventDefault();
  }
}
