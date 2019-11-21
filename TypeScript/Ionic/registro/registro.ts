import { Component } from "@angular/core";
import { NavController, NavParams, AlertController } from "ionic-angular";
import { AuthProvider } from "../../providers/auth/auth";
import { AplicacionProvider } from "../../providers/aplicacion/aplicacion";
import { UsuarioProvider } from "../../providers/usuario/usuario";
import { RegistrarUsuarioRequest } from "../../models/registrar-usuario-request";
import { LocationProvider } from "../../providers/location/location";
import { FormBuilder, Validators, FormGroup } from "@angular/forms";
import { LoginComponent } from "../../components/login/login";
import { ConsoleProvider } from "../../providers/console/console";

@Component({
  selector: "registro",
  templateUrl: "registro.html"
})
export class RegistroComponent {
  registerForm: FormGroup;
  user: RegistrarUsuarioRequest = new RegistrarUsuarioRequest({
    nombre: "",
    apellido: "",
    Email: "",
    Password: "",
    ConfirmPassword: "",
    latitud: null,
    longitud: null,
    accuracy: null,
    altitud: null,
    altitudAccuracy: null
  });
  loading: boolean = false;

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
    this.registerForm = formBuilder.group({
      nombre: ["", Validators.required],
      apellido: ["", Validators.required],
      Email: ["", [Validators.required, Validators.email]],
      Password: ["", [Validators.required, Validators.minLength(6)]],
      ConfirmPassword: ["", Validators.required]
    });
  }

  ionViewDidLoad() {
    this.ap.analitycs.then(ga => {
      ga.trackView("RegistroComponent")
        .then(() => {})
        .catch(e => this.console.log(e));
    });
    this.loading = false;
  }

  register() {
    this.user.Password = this.registerForm.get("Password").value;
    this.user.ConfirmPassword = this.registerForm.get("ConfirmPassword").value;
    if (this.user.Password != this.user.ConfirmPassword) {
      let alert = this.alertCtrl.create({
        subTitle: "Las contraseñas deben coincidir",
        buttons: ["Aceptar"]
      });
      alert.present();
      return;
    }
    this.loading = true;
    const pos = this.location.ultimaLocation();
    this.user.nombre = this.registerForm.get("nombre").value;
    this.user.apellido = this.registerForm.get("apellido").value;
    this.user.Email = this.registerForm.get("Email").value;
    this.user.latitud = pos ? pos.latitud : null;
    this.user.longitud = pos ? pos.longitud : null;
    this.user.accuracy = pos ? pos.accuracy : null;
    this.user.altitud = pos ? pos.altitud : null;
    this.user.altitudAccuracy = pos ? pos.altitudAccuracy : null;
    this.auth.registerUser(this.user).subscribe(
      () => {
        this.loading = false;
        this.login();
      },
      response => {
        this.ap.manejarError(response, this.alertCtrl);
        this.loading = false;
      }
    );
  }

  login() {
    this.loading = true;
    this.auth.loginUser(this.user.Email, this.user.Password).subscribe(
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

  volver() {
    this.navCtrl.setRoot(LoginComponent);
  }

  ionViewWillLeave() {
    this.loading = false;
  }
}
