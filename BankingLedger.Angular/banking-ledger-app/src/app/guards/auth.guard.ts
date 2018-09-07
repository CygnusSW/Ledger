import { Injectable } from '@angular/core';
import { CanActivate, CanActivateChild, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from "../auth/auth.service";
import { Router } from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate, CanActivateChild {
  constructor(
    private _authService : AuthService,
    private _router : Router  
  )
  {
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot) {
      var result = this._authService.isAuthed(); 
      if (result)
        return result;
      
        this._router.navigate(["login"]);
        return false;
  }

  canActivateChild()
  { 
    var result = this._authService.isAuthed(); 
    if (result)
      return result;
    
      this._router.navigate(["login"]);
      return false;
  }
}
