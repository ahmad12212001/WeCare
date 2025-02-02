import { Component, OnInit, AfterViewInit, Inject, OnChanges } from '@angular/core';
import { Observable, map } from 'rxjs';
import { LayoutService } from '../../services/layout.service';
import { NavService } from '../../services/nav.service';
import { SwitcherService } from '../../services/switcher.service';
import { AuthorizeService } from '../../../../api-authorization/authorize.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  public isCollapsed = true;
  public userName?: Observable<string | null | undefined>;
  constructor(
    private layoutService: LayoutService,
    public navServices: NavService,
    private switcherService: SwitcherService,
    private authorizeService: AuthorizeService
  ) { }

  ngOnInit(): void {
    this.userName = this.authorizeService.getUser().pipe(map(u => u && u.name));
  }

  toggleSidebar() {
    debugger
    if ((this.navServices.collapseSidebar = !this.navServices.collapseSidebar)) {
      document.querySelector('.app')?.classList.add('sidenav-toggled');
    }
    else {
      document.querySelector('.app')?.classList.remove('sidenav-toggled');
    }
  }

  toggleSidebarNotification() {
    if (document.querySelector('.sidebar-right')?.classList.contains('sidebar-open')) {
      this.layoutService.emitSidebarNotifyChange(false);
    } else {
      this.layoutService.emitSidebarNotifyChange(true);
    }
  }

  toggleSwitcher() {
    if (document.querySelector('.demo_changer')?.classList.contains('active')) {
      this.switcherService.emitSwitcherChange(false);
    } else {
      this.switcherService.emitSwitcherChange(true);
    }
  }
}
