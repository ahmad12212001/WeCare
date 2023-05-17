import { AfterViewInit, Input } from '@angular/core';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { NgbActiveModal, NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { ReplaySubject, Subject, Subscription } from 'rxjs';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';
import { Alert, NotificationType } from './alert';


@Component({
  selector: 'alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.scss']
})
export class AlertComponent implements OnInit, AfterViewInit {
  ngOnDestroy(): void {
    this.onDestroy$.next();
    this.onDestroy$.complete();
  }
  protected onDestroy$ = new ReplaySubject<void>();
  public alert!: Alert;
  @Input() id: string = 'default-alert';
  @Input() fade = true;
  @Input() modalRef!: NgbModalRef;
  note = new FormControl('', Validators.required);
  public submitAction$ = new Subject<any>();
  alertSubscription!: Subscription;
  notificationTypes!: any;
  invalid: boolean = false;
  constructor(public activeModal: NgbActiveModal) { }
  ngAfterViewInit(): void {
  }

  ngOnInit() {
    this.notificationTypes = NotificationType;
    if (this.alert.autoClose) {
      setTimeout(() => this.closeAlert(), this.alert.duration ? this.alert.duration : 3000);
    }
    this.note.valueChanges.pipe(takeUntil(this.onDestroy$), debounceTime(1000), distinctUntilChanged()).subscribe(res => {
      this.invalid = false;
    })
  }

  public onCancel(): void {
    this.activeModal.close();
  }

  closeAlert() {
    this.modalRef.close();
  }
  cssClass(alert: Alert) {
    if (!alert) return "";

    const classes = ['alert', 'alert-dismissable'];

    const alertTypeClass = {
      [NotificationType.Success]: 'col-12 modal-title font-weight-bolder text-success',
      [NotificationType.Error]: 'col-12 modal-title font-weight-bolder text-danger',
      [NotificationType.Info]: 'col-12 modal-title font-weight-bolder text-dark',
      [NotificationType.Warning]: 'col-12 modal-title font-weight-bolder text-warning',
      [NotificationType.Note]: 'col-12 modal-title font-weight-bolder text-danger ',
    }

    classes.push(alertTypeClass[alert.type]);

    return classes.join(' ');
  }
  submitAction() {
    if (this.alert.type === this.notificationTypes.Note) {
      if (this.note.invalid) {
        this.invalid = true;

      } else {
        this.invalid = false;
        this.submitAction$.next({ note: this.note.value, status: true });
      }
    } else {
      this.submitAction$.next(true);
      this.modalRef.close();
    }

  }
  close() {
    this.modalRef.close();
  }
}

