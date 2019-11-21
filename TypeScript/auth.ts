import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs/BehaviorSubject";
import { Storage } from "@ionic/storage";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import { AplicacionProvider } from "../aplicacion/aplicacion";
import { RegistrarUsuarioRequest } from "../../models/registrar-usuario-request";
import { OfflineModeProvider } from "../offline-mode/offline-mode";
import { RestorePasswordRequest } from "../../models/restore-password-request";

const TOKEN_KEY = "tatuel.auth-token";
const GRANT_TYPE = "tatuel.auth-grant-type";
const USER_DATA = "tatuel.auth-user";

@Injectable()
export class AuthProvider {
  authenticationState = new BehaviorSubject(null);
  private url: string;
  private token: string;
  private userData: any;
  private userDataSubject = new BehaviorSubject(null);

  constructor(
    private storage: Storage,
    private http: HttpClient,
    private ap: AplicacionProvider,
    private offlineMode: OfflineModeProvider
  ) {
    this.url = this.ap.urlApi;
    this.checkToken();
  }

  // Login de usuario
  loginUser(email: string, password: string): Observable<any> {
    const params = `grant_type=password&username=${email}&password=${password}`;
    return this.http.post(`${this.url}/Token`, params, {
      headers: new HttpHeaders({
        "Content-Type": "application/x-www-form-urlencoded",
        "Access-Control-Allow-Origin": "*"
      })
    });
  }

  registerUser(params: RegistrarUsuarioRequest) {
    return this.http.post(`${this.url}/api/Account/Register`, params);
  }

  sendVerificacionCode(email: string) {
    const params = {
      email: email
    };
    return this.http.post(`${this.url}/api/Account/SendTokenForgetPassword`, params);
  }

  recuperarPassword(params: RestorePasswordRequest) {
    return this.http.post(`${this.url}/api/Account/RestorePassword`, params);
  }

  logout() {
    this.offlineMode.setOfflineMode(false);
    return this.storage.remove(TOKEN_KEY).then(res => {
      this.authenticationState.next(false);
    });
  }

  isAuthenticated() {
    return this.authenticationState.asObservable();
  }

  checkToken() {
    this.storage.get(USER_DATA).then(data => {
      if (data) {
        this.userData = data;
        this.userDataSubject.next(this.userData);
      }
    });
    return this.storage.get(TOKEN_KEY).then(res => {
      if (res) {
        this.token = res;
        this.authenticationState.next(true);
      } else {
        this.authenticationState.next(false);
      }
    });
  }

  setToken(token: string) {
    this.token = token;
    this.storage.set(TOKEN_KEY, token).then(res => {
      this.authenticationState.next(true);
    });
  }

  getLastToken(): string {
    return this.token;
  }

  getToken(): Promise<any> {
    return this.storage.get(TOKEN_KEY).then(
      data => {
        return data;
      },
      error => console.error(error)
    );
  }

  getTokenAsObservable(): Observable<any> {
    return Observable.fromPromise(this.getToken());
  }

  setGrantType(grantType: string) {
    this.storage.set(GRANT_TYPE, grantType);
  }

  setUsuario(data: string) {
    this.storage.set(USER_DATA, data);
    this.userData = data;
    this.userDataSubject.next(this.userData);
    //this.location.setLocation(data);
  }

  getUserData(): Observable<any> {
    return this.userDataSubject.asObservable();
  }
}
