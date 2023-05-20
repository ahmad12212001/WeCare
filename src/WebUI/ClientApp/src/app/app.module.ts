import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from './shared/shared.module';
import { ApiAuthorizationModule } from '../api-authorization/api-authorization.module';
import { NgxPermissionsModule, NgxPermissionsService } from 'ngx-permissions';
import { AuthorizeService, UserInfo } from '@authorize/authorize.service';
import { lastValueFrom } from 'rxjs';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    SharedModule,
    ApiAuthorizationModule,
    ReactiveFormsModule,
    ModalModule.forRoot(),
    NgxPermissionsModule.forRoot()

  ],
  providers: [

    {
      provide: APP_INITIALIZER,
      useFactory: (authService: AuthorizeService, ps: NgxPermissionsService) => function () {
        return lastValueFrom(authService.getUserInfo()).then((user: UserInfo) => {
          ps.addPermission(user.role);
        })
      },
      deps: [AuthorizeService, NgxPermissionsService],
      multi: true
    },
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
