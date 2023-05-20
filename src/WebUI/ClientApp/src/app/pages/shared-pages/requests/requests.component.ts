import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { PaginatedList } from '@shared/models/paginated-list';
import { RequestDto } from '@app/pages/models/request-dtos';
import { RequestsService } from '@app/pages/services/requests.service';
import { FormBase } from '@shared/models/form-base';
import { AlertService } from '@shared/modules/alert/alert.service';
import { ColumnMode } from '@swimlane/ngx-datatable';
import { map, startWith, takeUntil } from 'rxjs';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { UserListTableComponent } from '@shared/user-list-table/user-list-table.component';
import { FeedBackComponent } from '@shared/modules/feed-back/feed-back.component';
import { MaterialDialogComponent } from '../materials/material-dialog/material-dialog.component';

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.scss']
})
export class RequestsComponent extends FormBase implements OnInit {
  rows: PaginatedList<RequestDto>;
  pageSize: number = 10;
  pageNumber: number = 1;
  name = new FormControl('');
  ColumnMode = ColumnMode;
  modalRef!: NgbModalRef;

  constructor(private _requestsService: RequestsService, private _alertService: AlertService, protected modalService: NgbModal) {
    super();
  }

  ngOnInit() {
    this.getRequestsPagination();


    this.name.valueChanges.pipe(startWith(''), map(value => {
      this.pageNumber = 1;
      this.getRequestsPagination();
    })).subscribe()
  }



  setPage(pageInfo) {
    this.getRequestsPagination();
  }

  getRequestsPagination() {
    this._requestsService.getRequestsPagination(this.pageNumber, this.pageSize, this.name.value).subscribe(pagedData => {
      this.rows = pagedData
    });
  }

  openMaterialsDialog(request: RequestDto) {
    this.modalRef = this.modalService.open(MaterialDialogComponent, {
      windowClass: "animated fadeInDown",
      size: 'lg',
      centered: true
    });
    const componentInstance: MaterialDialogComponent = this.modalRef.componentInstance;
    componentInstance.requestId = request.id;

  }

  deleteRequest(request: RequestDto) {
    this._alertService.modalSize = "md";
    this._alertService.warn('Are you sure you want to delete this Request !!!', request.description, {
      autoClose: false,
      showCloseButton: true,
      actionMessage: 'Yes',
      closeButtonText: 'No',
      cssClass: 'btn-white',
      closeButtonCss: 'btn-primary'
    }
    );

    this._alertService.submitAction$.pipe(takeUntil(this.onDestroy$)).subscribe(res => {
      if (res) {
        this._requestsService.deleteRequest(request.id).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
          if (res) {
            this.getRequestsPagination();
          }
        })
      }
    });
  }

  openUserList(id: number) {
    this.modalRef = this.modalService.open(UserListTableComponent, {
      windowClass: "animated fadeInDown",
      size: 'lg',
      centered: true
    });
    const componentInstance: UserListTableComponent = this.modalRef.componentInstance;
    componentInstance.title = "Volunteers";
    componentInstance.endPoint = `Volunteers?id=${id}`;
  }



  openFeedbackDialog(request: RequestDto) {
    this.modalRef = this.modalService.open(FeedBackComponent, {
      windowClass: "animated fadeInDown",
      size: 'lg',
      centered: true
    });
    const componentInstance: FeedBackComponent = this.modalRef.componentInstance;
    componentInstance.studentName = request?.studentName;
    componentInstance.requestId = request?.id;
    componentInstance.closeDialog$.asObservable().pipe(takeUntil(this.onDestroy$)).subscribe(res => {
      if (res) {
        this._requestsService.addRequestFeedback(res).pipe(takeUntil(this.onDestroy$)).subscribe(response => {
          if (response > 0) {
            request.hasFeedback = true;
            this.modalRef.dismiss();
          }
        })
      } else {
        this.modalRef.dismiss()
      }
    })
  }

  acceptRequest(request: RequestDto) {
    this._requestsService.acceptRequest(request).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
      if (res > 0) {
        request.hasRequested = true;
        request.hasFeedback = false;
      }
    })
  }

}