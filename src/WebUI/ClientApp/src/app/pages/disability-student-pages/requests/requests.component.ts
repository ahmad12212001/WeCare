import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { PaginatedList } from '@shared/models/paginated-list';
import { RequestDto } from '@app/pages/models/request-dtos';
import { RequestService } from '@app/pages/services/request.service';
import { FormBase } from '@shared/models/form-base';
import { AlertService } from '@shared/modules/alert/alert.service';
import { ColumnMode } from '@swimlane/ngx-datatable';
import { map, startWith, takeUntil } from 'rxjs';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { CustomDialogComponent } from '@shared/modules/custom-dialog/custom-dialog.component';
import { UserListTableComponent } from '@shared/user-list-table/user-list-table.component';
import { FeedBackComponent } from '@shared/modules/feed-back/feed-back.component';

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.scss']
})
export class RequestsComponent extends FormBase implements OnInit {

  columns = [
    { name: 'Id' },
    { name: 'Name' },
    { name: 'DueDate' },
    { name: 'Location' },
    { name: 'HallNo' }
  ];

  rows: PaginatedList<RequestDto>;
  pageSize: number = 10;
  pageNumber: number = 1;
  name = new FormControl('');
  ColumnMode = ColumnMode;
  modalRef!: NgbModalRef;

  constructor(private _requestService: RequestService, private _alertService: AlertService,
    protected modalService: NgbModal) {
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
    this._requestService.getRequestsPagination(this.pageNumber, this.pageSize, this.name.value).subscribe(pagedData => {
      this.rows = pagedData
    });
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
        this._requestService.deleteRequest(request.id).pipe(takeUntil(this.onDestroy$)).subscribe(res => {
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
    componentInstance.volunteerName = request?.volunteerName;
  }
}