import { Injectable } from "@angular/core";
import { SQLite, SQLiteObject } from "@ionic-native/sqlite";
import { PlantillaCompuestaResponse } from "../../models/plantilla-compuesta-response";
import { InstanciaPlantillaResponse } from "../../models/instancia-plantilla-response";
import { ConsoleProvider } from "../console/console";
import { InitialDataProvider } from "../initial-data/initial-data";

const SCRIPT_DROP_TABLE_REQUEST = "DROP TABLE IF EXISTS requests;";
const SCRIPT_DROP_TABLE_PLANTILLA_COMPUESTA_RESPONSE = "DROP TABLE IF EXISTS plantillas;";
const SCRIPT_DROP_TABLE_INSTANCIA_PLANTILLA_RESPONSE = "DROP TABLE IF EXISTS instancias;";

const SCRIPT_CREATE_TABLE_REQUEST = "CREATE TABLE IF NOT EXISTS requests (id TEXT PRIMARY KEY, url TEXT, type TEXT, data TEXT, time INTEGER, send BOOLEAN)";
const SCRIPT_GET_ALL_REQUEST = "SELECT * FROM requests ORDER BY time DESC ";
const SCRIPT_INSERT_REQUEST = "INSERT OR REPLACE INTO requests(id, url, type, data,  time, send) VALUES(?,?,?,?,?,?)";
const SCRIPT_DELETE_REQUEST = "DELETE FROM requests WHERE id=?";
const SCRIPT_DELETE_ALL_REQUEST = "DELETE FROM requests";

const SCRIPT_CREATE_TABLE_PLANTILLA_COMPUESTA_RESPONSE = "CREATE TABLE IF NOT EXISTS plantillas (id TEXT PRIMARY KEY, plantilla TEXT, timestamp DATETIME DEFAULT CURRENT_TIMESTAMP NOT NULL)";
const SCRIPT_INSERT_PLANTILLA_COMPUESTA_RESPONSE = "INSERT OR REPLACE INTO plantillas (id, plantilla) VALUES(?,?)";
const SCRIPT_GET_ALL_PLANTILLA_COMPUESTA_RESPONSE = "SELECT * FROM plantillas ORDER BY timestamp DESC ";
const SCRIPT_DELETE_ALL_PLANTILLA_COMPUESTA_RESPONSE = "DELETE FROM plantillas";
const SCRIPT_DELETE_PLANTILLA_COMPUESTA_RESPONSE = "DELETE FROM plantillas where id = ?";

const SCRIPT_CREATE_TABLE_INSTANCIA_PLANTILLA_RESPONSE = "CREATE TABLE IF NOT EXISTS instancias (id_plantilla INTEGER PRIMARY KEY, instancias TEXT)";
const SCRIPT_INSERT_INSTANCIA_PLANTILLA_RESPONSE = "INSERT OR REPLACE INTO instancias (id_plantilla, instancias) VALUES(?,?)";
const SCRIPT_FIND_BY_PLANTILLA_INSTANCIA_PLANTILLA_RESPONSE = "SELECT * FROM instancias where id_plantilla = ?";
const SCRIPT_DELETE_ALL_INSTANCIA_PLANTILLA_RESPONSE = "DELETE FROM instancias";
const SCRIPT_DELETE_INSTANCIA_PLANTILLA_RESPONSE = "DELETE FROM instancias where id_plantilla = ?";

const SQL_DB_NAME = "database.db";
const DEFAULT = "default";

export interface StoredRequest {
  url: string;
  type: string;
  data: any;
  time: number;
  id: string;
  send: boolean;
}

@Injectable()
export class LocaldatabaseProvider {
  db: SQLiteObject = null;
  requestForBrowsers: StoredRequest[] = [];

  constructor(private console: ConsoleProvider, private initialData: InitialDataProvider) {}

  setDatabase(sqlite: SQLiteObject) {
    this.console.log("setDatabase", sqlite);
    if (this.db === null) {
      this.db = sqlite;
    }
  }

  /**
   * @param sqlite
   */
  public createDatabase(sqlite: SQLite) {
    this.console.log("createDatabase", sqlite);
    return sqlite.create({
      name: SQL_DB_NAME,
      location: DEFAULT // the location field is required
    });
  }

  dropTables() {
    this.console.log("dropTables");
    var promises: Promise<any>[] = [];
    promises.push(this.execute(SCRIPT_DROP_TABLE_REQUEST));
    promises.push(this.execute(SCRIPT_DROP_TABLE_PLANTILLA_COMPUESTA_RESPONSE));
    promises.push(this.execute(SCRIPT_DROP_TABLE_INSTANCIA_PLANTILLA_RESPONSE));
    return Promise.all(promises);
  }

  createTables() {
    this.console.log("createTables");
    var promises: Promise<any>[] = [];
    promises.push(this.execute(SCRIPT_CREATE_TABLE_REQUEST));
    promises.push(this.execute(SCRIPT_CREATE_TABLE_PLANTILLA_COMPUESTA_RESPONSE));
    promises.push(this.execute(SCRIPT_CREATE_TABLE_INSTANCIA_PLANTILLA_RESPONSE));
    return Promise.all(promises);
  }

  dropAndCreateTables() {
    this.console.log("dropAndCreateTables");
    this.dropTables().then(() => {
      return this.createTables();
    });
  }

  execute(sql: string): Promise<any> {
    const params = [];
    this.console.info("sql", { sql, params });
    if (!this.db) return Promise.resolve(true);
    else return this.db.executeSql(sql, params);
  }

  getAllStoredRequest(): Promise<StoredRequest[]> {
    this.console.log("getAllStoredRequest");
    if (!this.db) {
      return Promise.resolve(this.requestForBrowsers);
    }
    const params = [];
    this.console.info("sql", { SCRIPT_GET_ALL_REQUEST, params });
    return this.db
      .executeSql(SCRIPT_GET_ALL_REQUEST, params)
      .then(response => {
        let requests = [];
        for (let index = 0; index < response.rows.length; index++) {
          const ob: StoredRequest = response.rows.item(index);
          ob.data = JSON.parse(ob.data);
          requests.push(ob);
        }
        this.console.log("requests", requests);
        return Promise.resolve(requests);
      })
      .catch(error => {
        this.console.error("getAllStoredRequest", error);
        return Promise.reject(error);
      });
  }

  saveStoredRequest(request: StoredRequest) {
    this.console.log("saveStoredRequest", request);
    const params = [request.id, request.url, request.type, JSON.stringify(request.data), request.time, request.send];
    this.console.info("sql", { SCRIPT_INSERT_REQUEST, params });
    if (!this.db){
      this.requestForBrowsers.push(request);
      return Promise.resolve(true);
    }else{
      return this.db.executeSql(SCRIPT_INSERT_REQUEST, params);
    }
  }

  deleteStoredRequest(request: StoredRequest) {
    this.console.log("deleteStoredRequest", request);
    const params = [request.id];
    this.console.info("sql", { SCRIPT_DELETE_REQUEST, params });
    if (!this.db) {
      this.requestForBrowsers.splice(this.requestForBrowsers.indexOf(request,0),1);
      return Promise.resolve(true);
    }
    return this.db.executeSql(SCRIPT_DELETE_REQUEST, params);
  }

  deleteAllStoredRequest() {
    this.console.log("deleteAllStoredRequest");
    const params = [];
    this.console.info("sql", { SCRIPT_DELETE_ALL_REQUEST, params });
    if (!this.db) return Promise.resolve(true);
    return this.db.executeSql(SCRIPT_DELETE_ALL_REQUEST, params);
  }

  /***************************************************************
   ****************** PlantillaCompuestaResponse *****************
   ****************************************************************/

  savePlantillasCompuestaResponse(plantillas: PlantillaCompuestaResponse[]) {
    this.console.log("savePlantillasCompuestaResponse", plantillas);
    var promises = [];
    for (let i = 0; i < plantillas.length; i++) {
      const element: PlantillaCompuestaResponse = plantillas[i];
      promises.push(this.savePlantillaCompuestaResponse(element));
    }
    Promise.all(promises).then(() => {
      this.initialData.saved();
    });
    return Promise.all(promises);
  }

  savePlantillaCompuestaResponse(plantilla: PlantillaCompuestaResponse) {
    return this.deletePlantillaCompuestaResponse(plantilla.idTemporal).then(() => {
      this.console.log("savePlantillaCompuestaResponse", plantilla);
      const params = [plantilla.idTemporal, JSON.stringify(plantilla)];
      this.console.info("sql", { SCRIPT_INSERT_PLANTILLA_COMPUESTA_RESPONSE, params });
      if (!this.db) return Promise.resolve(null);
      return this.db.executeSql(SCRIPT_INSERT_PLANTILLA_COMPUESTA_RESPONSE, params);
    });
  }

  getAllPlantillaCompuestaResponse(): Promise<PlantillaCompuestaResponse[]> {
    this.console.log("getAllPlantillaCompuestaResponse");
    if (!this.db) return Promise.resolve(null);
    const params = [];
    this.console.info("sql", { SCRIPT_GET_ALL_PLANTILLA_COMPUESTA_RESPONSE, params });
    return this.db
      .executeSql(SCRIPT_GET_ALL_PLANTILLA_COMPUESTA_RESPONSE, params)
      .then(response => {
        let plantillas: PlantillaCompuestaResponse[] = [];
        for (let index = 0; index < response.rows.length; index++) {
          const ob = response.rows.item(index);
          const pcr: PlantillaCompuestaResponse = JSON.parse(ob.plantilla);
          plantillas.push(pcr);
        }
        this.console.log("plantillas", plantillas);
        return Promise.resolve(plantillas);
      })
      .catch(error => {
        this.console.error("getAllPlantillaCompuestaResponse", error);
        return Promise.reject(error);
      });
  }

  deleteAllPlantillaCompuestaResponse() {
    this.console.log("deleteAllPlantillaCompuestaResponse");
    const params = [];
    this.console.info("sql", { SCRIPT_DELETE_ALL_PLANTILLA_COMPUESTA_RESPONSE, params });
    this.initialData.deleted();
    if (!this.db) return Promise.resolve(null);
    return this.db.executeSql(SCRIPT_DELETE_ALL_PLANTILLA_COMPUESTA_RESPONSE, params);
  }

  deletePlantillaCompuestaResponse(idTemporalPlantillaCompuesta: string) {
    this.console.log("deletePlantillaCompuestaResponse");
    const params = [idTemporalPlantillaCompuesta];
    this.console.info("sql", { SCRIPT_DELETE_PLANTILLA_COMPUESTA_RESPONSE, params });
    if (!this.db) return Promise.resolve(null);
    return this.db.executeSql(SCRIPT_DELETE_PLANTILLA_COMPUESTA_RESPONSE, params);
  }
  /***************************************************************
   ****************** InstanciaPlantillaResponse *****************
   ****************************************************************/
  saveInstanciasPlantillaResponse(idTemporalPlantillaCompuesta: string, instancias: InstanciaPlantillaResponse[]) {
    return this.deleteInstanciaPlantillaResponse(idTemporalPlantillaCompuesta).then(() => {
      this.console.log("saveInstanciasPlantillaResponse", { idTemporalPlantillaCompuesta, instancias });
      const params = [idTemporalPlantillaCompuesta, JSON.stringify(instancias)];
      this.console.info("sql", { SCRIPT_INSERT_INSTANCIA_PLANTILLA_RESPONSE, params });
      if (!this.db) return Promise.resolve(null);
      return this.db.executeSql(SCRIPT_INSERT_INSTANCIA_PLANTILLA_RESPONSE, params);
    });
  }

  getInstanciasPlantillaResponse(idPlantilla: number): Promise<InstanciaPlantillaResponse[]> {
    this.console.log("getInstanciasPlantillaResponse");
    if (!this.db) return Promise.resolve(null);
    const params = [idPlantilla];
    this.console.info("sql", { SCRIPT_FIND_BY_PLANTILLA_INSTANCIA_PLANTILLA_RESPONSE, params });
    return this.db
      .executeSql(SCRIPT_FIND_BY_PLANTILLA_INSTANCIA_PLANTILLA_RESPONSE, params)
      .then(response => {
        let instancias: InstanciaPlantillaResponse[] = [];
        if (response.rows.length > 0) {
          const ob = response.rows.item(0);
          instancias = JSON.parse(ob.instancias);
        }
        this.console.log("instancias", instancias);
        return Promise.resolve(instancias);
      })
      .catch(error => {
        this.console.error("getInstanciasPlantillaResponse", error);
        return Promise.reject(error);
      });
  }

  deleteAllInstanciaPlantillaResponse() {
    this.console.log("deleteAllInstanciaPlantillaResponse");
    const params = [];
    this.console.info("sql", { SCRIPT_DELETE_ALL_INSTANCIA_PLANTILLA_RESPONSE, params });
    if (!this.db) return Promise.resolve(null);
    return this.db.executeSql(SCRIPT_DELETE_ALL_INSTANCIA_PLANTILLA_RESPONSE, params);
  }

  deleteInstanciaPlantillaResponse(idTemporalPlantillaCompuesta: string) {
    this.console.log("deleteInstanciaPlantillaResponse");
    const params = [idTemporalPlantillaCompuesta];
    this.console.info("sql", { SCRIPT_DELETE_INSTANCIA_PLANTILLA_RESPONSE, params });
    if (!this.db) return Promise.resolve(null);
    return this.db.executeSql(SCRIPT_DELETE_INSTANCIA_PLANTILLA_RESPONSE, params);
  }
}
