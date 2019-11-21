import { Component } from "@angular/core";
import { AplicacionProvider } from "../../providers/aplicacion/aplicacion";
import { ConsoleProvider } from "../../providers/console/console";
import { NavController, NavParams, AlertController } from "ionic-angular";
import { AuthProvider } from "../../providers/auth/auth";
import { UsuarioProvider } from "../../providers/usuario/usuario";
import { SplashScreen } from "@ionic-native/splash-screen";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { LocationProvider } from "../../providers/location/location";
import { RestorePasswordRequest } from "../../models/restore-password-request";

@Component({
  selector: "recuperar-password",
  templateUrl: "recuperar-password.html"
})
export class RecuperarPasswordComponent {
  restoreForm: FormGroup;
  params: RestorePasswordRequest = new RestorePasswordRequest({
    Email: "",
    VerificationCode: "",
    NewPassword: "",
    ConfirmPassword: ""
  });
  loading: boolean = false;
  requestEmail = true;
  requestVerificationCode = false;
  requestNewPassword = false;

  constructor(
    public navCtrl: NavController,
    public navParams: NavParams,
    public auth: AuthProvider,
    public alertCtrl: AlertController,
    private usuarioProvider: UsuarioProvider,
    private ap: AplicacionProvider,
    formBuilder: FormBuilder,
    private location: LocationProvider,
    private console: ConsoleProvider
  ) {
    this.restoreForm = formBuilder.group({
      email: ["", [Validators.required, Validators.email]],
      verificationCode: ["", [Validators.required]],
      password: ["", [Validators.required, Validators.minLength(6)]],
      confirmPassword: ["", Validators.required]
    });
  }

  ionViewDidLoad() {
    this.ap.analitycs.then(ga => {
      ga.trackView("RecuperarPasswordComponent")
        .then(() => {})
        .catch(e => this.console.log(e));
    });
    this.loading = false;
    this.requestEmail = true;
    this.requestVerificationCode = false;
    this.requestNewPassword = false;
  }

  setVerificationCode(){
    this.requestEmail = false;
    this.requestVerificationCode = false;
    this.requestNewPassword = true;
  }

  restore() {
    this.params.Email = this.restoreForm.get("email").value;
    this.params.VerificationCode = this.restoreForm.get("verificationCode").value;
    this.params.NewPassword = this.restoreForm.get("password").value;
    this.params.ConfirmPassword = this.restoreForm.get("confirmPassword").value;
    if (this.params.NewPassword != this.params.ConfirmPassword) {
      let alert = this.alertCtrl.create({
        subTitle: "Las contraseñas deben coincidir",
        buttons: ["Aceptar"]
      });
      alert.present();
      return;
    }
    this.loading = true;
    this.auth.recuperarPassword(this.params).subscribe(
      (data) => {
        this.loading = false;
        if (data){
          this.login();
        }else{
          let alert = this.alertCtrl.create({
            subTitle: "El código de validación ha caducado",
            buttons: ["Aceptar"]
          });
          alert.present();
          this.requestEmail = false;
          this.requestVerificationCode = false;
          this.requestNewPassword = false;
          return;
        }
      },
      response => {
        this.ap.manejarError(response, this.alertCtrl);
        this.loading = false;
      }
    );
  }

  sendMail(){
    this.params.Email = this.restoreForm.get("email").value;
    this.loading = true;
    this.auth.sendVerificacionCode(this.params.Email).subscribe(
      () => {
        this.loading = false;
        let alert = this.alertCtrl.create({
          subTitle: "Enviamos un código de verificación a tu cuenta de correo",
          buttons: [{
            text: "Aceptar",
            handler: (data: any) => {
              this.requestEmail = false;
              this.requestVerificationCode = true;
              this.requestNewPassword = false;
            }
          }]
        });
        alert.present();
        return;
      },
      response => {
        this.ap.manejarError(response, this.alertCtrl);
        this.loading = false;
      }
    );
  }

  login() {
    this.loading = true;
    this.auth.loginUser(this.params.Email, this.params.NewPassword).subscribe(
      response => {
        this.auth.setGrantType(response.token_type);
        this.auth.setToken(response.access_token);
        this.usuarioProvider.getUsuario(response.token_type).subscribe(response => {
          this.auth.setUsuario(response);
        });
        this.loading = false;
      },
      response => {
        this.ap.manejarError(response, this.alertCtrl);
        this.loading = false;
      }
    );
  }

  ionViewWillLeave() {
    this.loading = false;
  }
}
