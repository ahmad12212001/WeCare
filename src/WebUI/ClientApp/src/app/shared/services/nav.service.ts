import { Injectable, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, fromEvent, Subject } from 'rxjs';
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
        private router: Router
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
        { headTitle: 'MAIN' },
        {
            path: '/requests', title: 'Requests', type: 'link', icon: 'list', active: false
        },
        {
            path: '/courses', title: 'Courses', type: 'link', icon: 'list', active: false
        },
        {
            path: '/majors', title: 'Majors', type: 'link', icon: 'list', active: false
        }

    ];
    VOlUNTEERSTUDENTMENUITEMS: Menu[] = [
        { headTitle: 'MAIN' },
        {
            path: '/requests', title: 'Requests', type: 'link', icon: 'list', active: false
        },
   

    ];
    ACADEMICSTAFFMENUITEMS: Menu[] = [
        { headTitle: 'MAIN' },
        {
            path: '/requests', title: 'Requests', type: 'link', icon: 'list', active: false
        },
        {
            path: '/courses', title: 'Courses', type: 'link', icon: 'list', active: false
        }
   

    ];

    DEANOFFICEMENUITEMS: Menu[] = [
        { headTitle: 'MAIN' },
        {
            path: '/requests', title: 'Requests', type: 'link', icon: 'list', active: false
        },
        {
            path: '/courses', title: 'Courses', type: 'link', icon: 'list', active: false
        }
   

    ];
    

    

    //array
    items = new BehaviorSubject<Menu[]>(this.ACADEMICSTAFFMENUITEMS);
}