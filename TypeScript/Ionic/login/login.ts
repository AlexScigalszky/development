import { Component } from "@angular/core";
import { AlertController, NavController, NavParams } from "ionic-angular";

import { AplicacionProvider } from "../../providers/aplicacion/aplicacion";
import { AuthProvider } from "../../providers/auth/auth";
import { UsuarioProvider } from "../../providers/usuario/usuario";
import { RegistroComponent } from "../registro/registro";
import { RecuperarPasswordComponent } from "../recuperar-password/recuperar-password";
import { ConsoleProvider } from "../../providers/console/console";
import { SplashScreen } from "@ionic-native/splash-screen";

@Component({
  selector: "login",
  templateUrl: "login.html"
})
export class LoginComponent {
  user = {
    email: "",
    password: "",
    confirmPassword: ""
  };
  loading: boolean = false;
  tasks: any[] = [];

  constructor(
    public navCtrl: NavController,
    public navParams: NavParams,
    public auth: AuthProvider,
    public alertCtrl: AlertController,
    private usuarioProvider: UsuarioProvider,
    private ap: AplicacionProvider,
    private console: ConsoleProvider,
    private splash: SplashScreen
  ) {}

  ionViewDidLoad() {
    this.ap.analitycs.then(ga => {
      ga.trackView("LoginComponent")
        .then(() => {})
        .catch(e => this.console.log(e));
    });
    this.loading = false;
  }

  login() {
    if (!this.user.email || this.user.email.length == 0 || !this.user.password || this.user.password.length == 0) {
      return;
    }
    this.loading = true;
    this.auth.loginUser(this.user.email, this.user.password).subscribe(
      response => {
        this.splash.show();
        this.auth.setGrantType(response.token_type);
        this.auth.setToken(response.access_token);
        this.usuarioProvider.getUsuario(response.access_token).subscribe(response => {
          this.auth.setUsuario(response);
        });
      },
      response => {
        this.ap.manejarError(response, this.alertCtrl);
        this.loading = false;
      }
    );
  }

  recuperarPassword() {
    this.navCtrl.push(RecuperarPasswordComponent, {});
  }

  register() {
    this.navCtrl.push(RegistroComponent, {});
  }

  ionViewWillLeave() {
    this.loading = false;
  }
}
