import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { VideoBackgroundComponent } from './video-background/video-background.component';
import { HorizontallySplitWSidebarRightComponent } from './layouts/horizontally-split-w-sidebar-right/horizontally-split-w-sidebar-right.component';
import { LoginFormComponent } from './login/login-form/login-form.component';
import { ColorFilterComponent } from './color-filter/color-filter.component';
import { AuthService } from './auth/auth.service';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AngularFontAwesomeModule } from "angular-font-awesome";
import { AccountSummaryComponent } from './account-summary/account-summary.component';
import { SinglePanelComponent } from './layouts/single-panel/single-panel.component';
import { HNavBarComponent } from './h-nav-bar/h-nav-bar.component';
import { AuthGuard } from "./guards/auth.guard";
import { HttpInterceptorService } from "./auth/http-interceptor-service";

@NgModule({
  declarations: [
    AppComponent,
    VideoBackgroundComponent,
    HorizontallySplitWSidebarRightComponent,
    LoginFormComponent,
    ColorFilterComponent,
    AccountSummaryComponent,
    SinglePanelComponent,
    HNavBarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgbModule,
    AngularFontAwesomeModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpInterceptorService,
      multi: true
    },
    AuthService,
    AuthGuard

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
