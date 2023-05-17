import { Component, Input, OnChanges, OnInit, TemplateRef } from '@angular/core';
import { NgbActiveModal, NgbModalRef } from "@ng-bootstrap/ng-bootstrap";
import { FormBase } from '@shared/models/form-base';

@Component({
  selector: 'custom-dialog',
  templateUrl: './custom-dialog.component.html',
  styleUrls: ['./custom-dialog.component.scss']
})
export class CustomDialogComponent extends FormBase implements OnInit, OnChanges {
  @Input() bodyTemplate!: TemplateRef<any>;
  @Input() modalRef!: NgbModalRef;
  @Input() title!: string;
  @Input() subTitle!: string;
  constructor(public activeModal: NgbActiveModal) {
    super();
  }
  ngOnChanges(): void {
  }

  ngOnInit() {
  }
  public onCancel(): void {
    this.closeDialog$.next(false);
    this.activeModal.dismiss();
  }

  close() {
    this.closeDialog$.next(false);
    this.modalRef.close();
  }
}
