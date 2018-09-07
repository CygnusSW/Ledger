import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import * as alertify from "alertify.js";

@Component({
  selector: 'bl-h-nav-bar',
  templateUrl: './h-nav-bar.component.html',
  styleUrls: ['./h-nav-bar.component..scss']
})
export class HNavBarComponent implements OnInit {
  public logoLocation = "../../assets/imgs/logo_transparent.png";
  
  constructor(
    private _authService : AuthService
  ) { }

  ngOnInit() {
  }

  logout()
  {
    alertify.confirm("Are you sure you want to log out?",
      (btn) => {
        this._authService.Logout();
      },
      (err) => {
        console.error(err);
      }  
    )
  }

}
