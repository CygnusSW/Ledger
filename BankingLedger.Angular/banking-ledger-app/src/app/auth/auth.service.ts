import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { map } from "rxjs/operators";
import { Router } from '@angular/router';
import * as alertify from "alertify.js";
import * as moment from "moment";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private _http : HttpClient,
    private _router : Router
  )
  {}

  public Register(username: string, password: string, accountName : string) {
    return this._http.post(`${environment.apiRoot}/api/v1/register`, {
      Username: username,
      Password: password,
      AccountName: accountName
    });
  }

  public Login(username : string, password : string)
  {
    var headers = new HttpHeaders();
    headers.append('Content-Type', 'application/x-www-form-urlencoded');

    let urlSearchParams = new URLSearchParams();
    urlSearchParams.set('grant_type', 'password');
    urlSearchParams.set('username', username);
    urlSearchParams.set('password', password);
    let body = urlSearchParams.toString();

    return this._http.post(`${environment.apiRoot}/token`, body, {
      headers: headers
    })
    .pipe(
      map((res : any) => {
        if (sessionStorage)
        {          
          sessionStorage.setItem("authToken", res["access_token"]);
          sessionStorage.setItem("authTokenExpiration", res[".issued"]);
          sessionStorage.setItem("userName", res.userName);
        }
        return null;
      })
    )
  }

  public Logout()
  {
    this._http.post(`${environment.apiRoot}/api/v1/logout`, {})
    .subscribe(
      (succ) => {
        if (sessionStorage)
          sessionStorage.clear();
        
        this._router.navigate(["login"]);
      },
      (err) => {
        alertify.error(err.message);
        this._router.navigate(["login"]);
      }
    );
  }

  public isAuthed()
  {
    if (sessionStorage)
    {
      var rawExpDate = sessionStorage.getItem("authTokenExpiration");
      if (!rawExpDate)
        return false;
        
      var expDate = moment(rawExpDate, "ddd, DD MMM YYYY HH:mm:ss");
      if (expDate < moment().utc())
        return false;

      return true;
    }
    else //Check cookies
      return false;

  }

}
