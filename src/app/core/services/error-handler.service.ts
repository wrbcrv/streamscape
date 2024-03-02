import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService {

  constructor() { }

  handleErrors(error: any) {
    let errors: any = {};

    if (error && error.errors || error.error) {
      const object = error.errors || error.error;

      Object.keys(object).forEach(field => {
        errors[field] = object[field].join(' ');
      });
    }
    return errors;
  }
}