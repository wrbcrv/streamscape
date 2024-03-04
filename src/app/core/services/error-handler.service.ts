import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService {

  constructor() { }

  handleErrors(error: any) {
    let errors: any = {};

    if (error.error || error && error.errors) {
      const object = error.error || error.errors;

      Object.keys(object).forEach(field => {
        errors[field] = object[field].join(' ');
      });
    }

    return errors;
  }
}