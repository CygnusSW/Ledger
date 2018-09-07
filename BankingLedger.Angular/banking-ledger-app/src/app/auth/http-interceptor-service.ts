import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { AuthService } from './auth.service';
import { tap } from 'rxjs/operators';

@Injectable()
export class HttpInterceptorService implements HttpInterceptor {

  constructor(private _authService : AuthService) { }
  intercept(request: HttpRequest<any>, next: HttpHandler){    
      var authToken = sessionStorage.getItem("authToken");
    request = request.clone({
      setHeaders: {
        'Authorization': `bearer ${authToken}`
      }
    });    

    return next.handle(request)
      .pipe(
        tap(
          (next) => {},
          (err) => {     
            if (err instanceof HttpErrorResponse) {
                if (err.status === 401) {
                    this._authService.Logout();
                    if (sessionStorage)
                        sessionStorage.clear();
                    //else clear cookies
                }
            }
          }
        )
      )
    ;
  }
}