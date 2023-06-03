import { Injectable, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthorizeService, UserInfo } from '@authorize/authorize.service';
import { NgxPermissionsService } from 'ngx-permissions';
import { BehaviorSubject, fromEvent, lastValueFrom, Subject } from 'rxjs';
import { takeUntil, debounceTime } from 'rxjs/operators';

//Menu Bar
export interface Menu {
    headTitle?: string,
    path?: string;
    title?: string;
    icon?: string;
    type?: string;
    badgeType?: string;
    badgeValue?: string;
    badgeClass?: string;
    active?: boolean;
    children?: Menu[];
}

@Injectable({
    providedIn: 'root'
})

export class NavService implements OnDestroy {

    private unsubscriber: Subject<any> = new Subject();
    public screenWidth: BehaviorSubject<number> = new BehaviorSubject(window.innerWidth);

    public megaMenu: boolean = false;
    public megaMenuCollapse: boolean = window.innerWidth < 1199 ? true : false;
    public collapseSidebar: boolean = window.innerWidth < 991 ? true : false;
    public fullScreen: boolean = false;
    constructor(
        private router: Router,
        private _ngxPermissionServices: NgxPermissionsService,
        private _authorizedService: AuthorizeService
    ) {
        this.setScreenWidth(window.innerWidth);
        fromEvent(window, 'resize').pipe(
            debounceTime(1000),
            takeUntil(this.unsubscriber)
        ).subscribe((evt: any) => {
            this.setScreenWidth(evt.target.innerWidth);
            if (evt.target.innerWidth < 991) {
                this.collapseSidebar = false;
                this.megaMenu = false;
            }
            if (evt.target.innerWidth < 1199) {
                this.megaMenuCollapse = true;
            }
        });
        if (window.innerWidth < 991) {
            this.router.events.subscribe(event => {
                this.collapseSidebar = false;
                this.megaMenu = false;
            });
        }
    }



    ngOnDestroy() {
        this.unsubscriber.next(true);
        this.unsubscriber.complete();
    }

    private setScreenWidth(width: number): void {
        this.screenWidth.next(width);
    }


    DISIBILITYSTUDENTMENUITEMS: Menu[] = [
        { headTitle: 'MENU' },
        {
            path: '/requests', title: 'Requests', type: 'link', icon: 'list', active: false
        },
        {
            path: '/courses', title: 'Courses', type: 'link', icon: 'list', active: false
        },
        {
            path: '/exams', title: 'Exams', type: 'link', icon: 'list', active: false
        }
        ,
        {
            path: '/materials', title: 'Mateirls', type: 'link', icon: 'list', active: false
        }

    ];

    VOlUNTEERSTUDENTMENUITEMS: Menu[] = [
        { headTitle: 'MENU' },
        {
            path: '/requests', title: 'Requests', type: 'link', icon: 'list', active: false
        },
        {
            path: '/courses', title: 'Courses', type: 'link', icon: 'list', active: false
        },
        {
            path: '/exams', title: 'Exams', type: 'link', icon: 'list', active: false
        }
        ,
        {
            path: '/materials', title: 'Mateirls', type: 'link', icon: 'list', active: false
        }


    ];

    ACADEMICSTAFFMENUITEMS: Menu[] = [
        { headTitle: 'MENU' },
        {
            path: '/requests', title: 'Requests', type: 'link', icon: 'list', active: false
        },
        {
            path: '/courses', title: 'Courses', type: 'link', icon: 'list', active: false
        },
        {
            path: '/exams', title: 'Exams', type: 'link', icon: 'list', active: false
        },
        {
            path: '/students', title: 'Students', type: 'link', icon: 'list', active: false
        },
        {
            path: '/materials', title: 'Mateirls', type: 'link', icon: 'list', active: false
        }


    ];

    DEANOFFICEMENUITEMS: Menu[] = [
        { headTitle: 'MENU' },
        {
            path: '/requests', title: 'Requests', type: 'link', icon: 'list', active: false
        },
        {
            path: '/courses', title: 'Courses', type: 'link', icon: 'list', active: false
        },
        {
            path: '/exams', title: 'Exams', type: 'link', icon: 'list', active: false
        },
        {
            path: '/students', title: 'Students', type: 'link', icon: 'list', active: false
        },
        {
            path: '/materials', title: 'Mateirls', type: 'link', icon: 'list', active: false
        },
        {
            path: '/users', title: 'Users', type: 'link', icon: 'list', active: false
        },
        {
            path: '/majors', title: 'Majors', type: 'link', icon: 'list', active: false
        },
        {
            path: '/major-groups', title: 'MajorGroups', type: 'link', icon: 'list', active: false
        }
    ];


    async getMenuItems() {
        await lastValueFrom(this._authorizedService.getUserInfo()).then((user: UserInfo) => {
            this._ngxPermissionServices.addPermission(user.role);
        })
        const hasDeanOfficePermission = await this._ngxPermissionServices.hasPermission('DeanOffice');
        const hasDisabiliotyStudentPermission = await this._ngxPermissionServices.hasPermission('DisabilityStudent');
        const hasVolunteerStudentPermission = await this._ngxPermissionServices.hasPermission('VolunteerStudent');
        const hasAcademicStaffPermission = await this._ngxPermissionServices.hasPermission('AcademicStaff');
        const hasAdminPermission = await this._ngxPermissionServices.hasPermission('Admin');

        if (hasDeanOfficePermission) {
            this.items = new BehaviorSubject<Menu[]>(this.DEANOFFICEMENUITEMS);
        }

        if (hasDisabiliotyStudentPermission) {
            this.items = new BehaviorSubject<Menu[]>(this.DISIBILITYSTUDENTMENUITEMS);
        }

        if (hasVolunteerStudentPermission) {
            this.items = new BehaviorSubject<Menu[]>(this.VOlUNTEERSTUDENTMENUITEMS);
        }

        if (hasAcademicStaffPermission) {
            this.items = new BehaviorSubject<Menu[]>(this.ACADEMICSTAFFMENUITEMS);
        }

        if (hasAdminPermission) {
            this.items = new BehaviorSubject<Menu[]>(this.DEANOFFICEMENUITEMS);
        }
    }

    //array
    items = new BehaviorSubject<Menu[]>([]);

}