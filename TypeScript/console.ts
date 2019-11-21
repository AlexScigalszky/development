import { Injectable } from "@angular/core";

const LOG_ENABLED = false;

@Injectable()
export class ConsoleProvider {

  log(message?: any, ...optionalParams: any[]): void {
    if (LOG_ENABLED) console.log(message, optionalParams);
  }
  info(message?: any, ...optionalParams: any[]): void {
    if (LOG_ENABLED) console.info(message, optionalParams);
  }
  debug(message?: any, ...optionalParams: any[]): void {
    if (LOG_ENABLED) console.debug(message, optionalParams);
  }
  error(message?: any, ...optionalParams: any[]): void {
    if (LOG_ENABLED) console.error(message, optionalParams);
  }
}
