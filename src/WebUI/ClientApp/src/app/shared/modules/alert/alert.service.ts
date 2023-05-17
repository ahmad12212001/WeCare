import { Injectable } from '@angular/core';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Observable, Subject, Subscription } from 'rxjs';
import { filter } from 'rxjs/operators';
import { Alert, NotificationType } from './alert';
import { AlertComponent } from './alert.component';


@Injectable()
export class AlertService {
  private subject = new Subject<Alert>();
  private defaultId = 'default-alert';
  modalSize: string = 'sm';
  alertSubscription!: Subscription;
  modalRef!: NgbModalRef;
  submitAction$ = new Subject<any>();
  constructor(protected modalService: NgbModal) {
    this.onInit();
  }

  onAlert(id = this.defaultId): Observable<Alert> {
    return this.subject.asObservable().pipe(filter(x => x && x.id === id));
  }

  success(header: string, body: string, options?: any) {
    this.alert(new Alert({ ...options, type: NotificationType.Success, header: header, body: body }));
  }

  error(header: string, body: string, options?: any) {
    this.alert(new Alert({ ...options, type: NotificationType.Error, header: header, body: body, cssClass: 'btn-danger' }));
  }

  info(header: string, body: string, options?: any) {
    this.alert(new Alert({ ...options, type: NotificationType.Info, header: header, body: body, cssClass: options.cssClass ? options.cssClass : 'btn-primary' }));
  }

  note(header: string, body: string, options?: any) {
    this.alert(new Alert({ ...options, type: NotificationType.Note, header: header, body: body, cssClass: options.cssClass ? options.cssClass : 'btn-primary' }));
  }

  warn(header: string, body: string, options?: any) {
    this.alert(new Alert({ ...options, type: NotificationType.Warning, header: header, body: body }));
  }

  delete(header: string, body: string, options?: any) {
    this.alert(new Alert({ ...options, type: NotificationType.Error, header: header, body: body, cssClass: 'btn-danger' }));
  }

  alert(alert: Alert) {
    alert.id = alert.id || this.defaultId;
    this.subject.next(alert);
  }

  clear(id = this.defaultId) {
    this.subject.next(new Alert({ id }));
  }

  onInit() {
    this.alertSubscription = this.onAlert(this.defaultId)
      .subscribe(alert => {
        if (alert.body) {
          this.modalRef = this.modalService.open(AlertComponent, {
            windowClass: "animated fadeInDown",
            size: this.modalSize,
            centered: true
          });
          const componentInstance: AlertComponent = this.modalRef.componentInstance;
          componentInstance.alert = alert;
          componentInstance.modalRef = this.modalRef;
          componentInstance.submitAction$.subscribe(res => {
            this.submitAction$.next(res);
          });
        }
      });
  }
}
