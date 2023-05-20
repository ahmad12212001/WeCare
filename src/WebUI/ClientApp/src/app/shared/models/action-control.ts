import { BehaviorSubject, Observable } from 'rxjs';

export class ActionControl {
    private _value$ = new BehaviorSubject<any>(null);
    private value$: Observable<any> = this._value$.asObservable();

    get value(): Observable<any> {
        return this.value$;
    }

    set value(value: any) {
        this._value$.next(value);
    }
}
