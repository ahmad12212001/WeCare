import { Component, OnDestroy } from "@angular/core";
import { Observable, ReplaySubject, Subject } from "rxjs";
@Component({
    template: ''
})
export abstract class FormBase implements OnDestroy {
    ngOnDestroy(): void {
        this.onDestroy$.next();
        this.onDestroy$.complete();
    }
    protected onDestroy$ = new ReplaySubject<void>();
    public onSubmit$ = new ReplaySubject<boolean | null>();
    public submitted$: Observable<boolean | null> = this.onSubmit$.asObservable();
    public closeDialog$ = new Subject<any>();
    public exportedDialogData$ = new Subject<any>();
    public searchDialogData$ = new Subject<any>();
}
