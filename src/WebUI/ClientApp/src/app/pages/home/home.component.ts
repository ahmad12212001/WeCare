import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { AuthorizeService } from '@authorize/authorize.service';
import { Observable, map } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class HomeComponent implements OnInit {
  public isAuthenticated?: Observable<boolean>;

  public userName?: Observable<string | null | undefined>;

  constructor(private authorizeService: AuthorizeService, private _router: Router) { }

  ngOnInit() {
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.userName = this.authorizeService.getUser().pipe(map(u => u && u.name));

    localStorage.setItem('reloaded', "false");
  }


}
