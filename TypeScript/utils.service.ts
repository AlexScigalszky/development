import { Injectable } from "@angular/core";
import { ValidatorFn, AbstractControl, Validators } from "@angular/forms";

@Injectable({
  providedIn: "root",
})
export class UtilsService {
  constructor() {}

  public sortBy(data: any[], field: string): any[] {
    return data.sort((a, b) => {
      if (!a.nombre || !b.nombre) {
        return -1;
      }
      return a.nombre.localeCompare(b.nombre);
    });
  }

  public remove<T>(array: T[], item: T) {
    const index: number = array.indexOf(item);
    if (index !== -1) {
      array.splice(index, 1);
    }
  }

  allowOnlyNumbers(eventOnKeyDown: KeyboardEvent) {
    if (
      [46, 8, 9, 27, 13].indexOf(eventOnKeyDown.keyCode) !== -1 || // Allow: Delete, Backspace, Tab, Escape, Enter
      (eventOnKeyDown.keyCode === 65 && eventOnKeyDown.ctrlKey === true) || // Allow: Ctrl+A
      (eventOnKeyDown.keyCode === 67 && eventOnKeyDown.ctrlKey === true) || // Allow: Ctrl+C
      (eventOnKeyDown.keyCode === 86 && eventOnKeyDown.ctrlKey === true) || // Allow: Ctrl+V
      (eventOnKeyDown.keyCode === 88 && eventOnKeyDown.ctrlKey === true) || // Allow: Ctrl+X
      (eventOnKeyDown.keyCode === 65 && eventOnKeyDown.metaKey === true) || // Cmd+A (Mac)
      (eventOnKeyDown.keyCode === 67 && eventOnKeyDown.metaKey === true) || // Cmd+C (Mac)
      (eventOnKeyDown.keyCode === 86 && eventOnKeyDown.metaKey === true) || // Cmd+V (Mac)
      (eventOnKeyDown.keyCode === 88 && eventOnKeyDown.metaKey === true) || // Cmd+X (Mac)
      (eventOnKeyDown.keyCode >= 35 && eventOnKeyDown.keyCode <= 39) // Home, End, Left, Right
    ) {
      return; // let it happen, don't do anything
    }
    if ((eventOnKeyDown.shiftKey || eventOnKeyDown.keyCode < 48 || eventOnKeyDown.keyCode > 57) && (eventOnKeyDown.keyCode < 96 || eventOnKeyDown.keyCode > 105)) {
      eventOnKeyDown.preventDefault();
    }
  }

  allowOnlyNumbersAndComma(eventOnKeyDown: KeyboardEvent) {
    if (eventOnKeyDown.key === ".") return;
    else this.allowOnlyNumbers(eventOnKeyDown);
  }

  isStringNumber(str) {
    var parsed = parseFloat(str);
    var casted = +str;
    return parsed === casted && !isNaN(parsed) && !isNaN(casted);
  }

  notWhiteSpaceValidator(): ValidatorFn {
    return Validators.pattern(".*\\S.*[a-zA-z0-9 ]");
  }

  onlyNumbersValidator(): ValidatorFn {
    return Validators.pattern(/^-?(0|[1-9]\d*)?$/);
  }

  forbiddenDateValidator(date: Date): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const controlValue: Date = new Date(control.value);
      const dateFromControl = new Date(controlValue.getFullYear(), controlValue.getMonth(), controlValue.getDate() + 1);
      const dateToCompare = new Date(date.getFullYear(), date.getMonth(), date.getDate());
      const forbidden = dateFromControl < dateToCompare;
      return forbidden ? { forbiddenDate: { value: control.value } } : null;
    };
  }

  getFirstDateOfLastMonth(): Date {
    const date = new Date();
    date.setDate(0); //zero based
    date.setMonth(date.getMonth() - 1);
    return date;
  }

  getFirstDateOfCurrentMonth(): Date {
    const date = new Date();
    const mesActual = date.getMonth();
    const anioActual = date.getFullYear();
    return new Date(anioActual, mesActual, 0); // zero based
  }

  /**
   * Convert long string date (ofen from Rest API) to a date string
   */
  getOnlyDate(dateStr: string | Date): string {
    const currentDate = new Date(dateStr);
    return currentDate.toISOString().substring(0, 10);
  }
  
  /**
   * Return only values witch match query in any of this fields inside element (logic op: OR, LIKE)
   * @param array Array of objects
   * @param fields properties of each item whats will by compared with query
   * @param query text to compare values (LIKE)
   */
  filterBy(array: any[], fields: string[], query: string): any[] {
    return array.filter((x) => {
      let rst: boolean = false;
      for (let i = 0; i < fields.length; i++) {
        const element = fields[i].toLocaleLowerCase();
        rst = rst || (x[element] && (x[element] + "").toLocaleLowerCase().indexOf(query) !== -1);
      }
      return rst;
    });
  }

  /**
   * Return values witch NOT match query in any of this fields inside element (logic op: OR, LIKE)
   * @param array Array of objects
   * @param fields properties of each item whats will by compared with query
   * @param query text to compare values (LIKE)
   */
  filterNotBy(array: any[], fields: string[], query: string): any[] {
    return array.filter((x) => {
      let rst: boolean = false;
      for (let i = 0; i < fields.length; i++) {
        const element = fields[i].toLocaleLowerCase();
        rst = rst || (x[element] && (x[element] + "").toLocaleLowerCase().indexOf(query) === -1);
      }
      return rst;
    });
  }

  /**
   * Return an array without duplicates
   * @param array Array of objects
   */
  removeDuplicates(array: any[]): any[] {
    return array.filter(function (elem, index, self) {
      return index === self.indexOf(elem);
    });
  }

  /**
   * Return count of days are between dates
   * @param first date
   * @param second date
   */
  calculateDiffInDays(first: Date, second: Date) {
    return Math.floor((Date.UTC(second.getFullYear(), second.getMonth(), second.getDate()) - Date.UTC(first.getFullYear(), first.getMonth(), first.getDate()) ) /(1000 * 60 * 60 * 24));
  }
}
