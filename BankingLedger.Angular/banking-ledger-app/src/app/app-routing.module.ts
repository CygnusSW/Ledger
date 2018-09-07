import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HorizontallySplitWSidebarRightComponent } from './layouts/horizontally-split-w-sidebar-right/horizontally-split-w-sidebar-right.component';
import { LoginFormComponent } from './login/login-form/login-form.component';
import { AccountSummaryComponent } from "./account-summary/account-summary.component";
import { SinglePanelComponent } from "./layouts/single-panel/single-panel.component";
import { AuthGuard } from "./guards/auth.guard";

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: "full" },
  { path: '', component: HorizontallySplitWSidebarRightComponent,
    children: [
      { path: 'login', component: LoginFormComponent}
    ]
  },
  { path: "", component: SinglePanelComponent, canActivate: [AuthGuard], canActivateChild: [AuthGuard],
    children: [
      {path: "summary", component: AccountSummaryComponent }
    ]
  }



];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
