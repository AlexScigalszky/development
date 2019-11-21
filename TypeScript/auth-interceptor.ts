import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AuthProvider } from "../auth/auth";
import { ConsoleProvider } from "../console/console";
import { HttpHandler, HttpEvent, HttpRequest, HttpInterceptor, HttpResponse, HttpErrorResponse } from "@angular/common/http";
import { mergeMap, map, catchError, timeout } from "rxjs/operators";
import { OfflineModeProvider } from "../offline-mode/offline-mode";

@Injectable()
export class AuthInterceptorProvider implements HttpInterceptor {
  private isOffline = false;

  constructor(private auth: AuthProvider, private console: ConsoleProvider, private offlineMode: OfflineModeProvider) {
    this.offlineMode.isInOfflineMode().subscribe((isOffline: boolean) => {
      this.isOffline = isOffline;
      this.console.debug("this.isOffline = ", this.isOffline);
    });
  }

  setHeaders(request: HttpRequest<any>, token: string) {
    if (token) {
      this.console.log("putting token", token);
      request = request.clone({ setHeaders: { Authorization: `Bearer ${token}` } });
    }

    if (!request.headers.has("Content-Type")) {
      this.console.log("putting Content-Type");
      request = request.clone({ headers: request.headers.set("Content-Type", "application/json") });
    }

    if (!request.headers.has("Access-Control-Allow-Origin")) {
      this.console.log("putting Access-Control-Allow-Origin");
      request = request.clone({ headers: request.headers.set("Access-Control-Allow-Origin", "*") });
    }
    return request;
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.console.log("intercept Request");
    return Observable.fromPromise(this.auth.getToken())
      .pipe(
        mergeMap(token => {
          request = this.setHeaders(request, token);
          if (this.isOffline) {
            this.console.debug("Send with timeout in 1");
            return next.handle(request).pipe(timeout(1));
          } else {
            this.console.debug("Send without timeout");
            return next.handle(request);
          }
        })
      )
      .pipe(
        map((event: HttpEvent<any>) => {
          if (event instanceof HttpResponse) {
            this.console.log("event--->>>", event);
          }
          return event;
        }),
        catchError((error: HttpErrorResponse) => {
          if (error.status === 401 || error.status === 403) {
            this.console.log("Error_Token_Expired: redirecting to login.");
            this.auth.logout();
          }
          this.console.error("--->>>", error);
          return Observable.throw(error);
        })
      );
  }
}
