import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService {

  constructor() { }

  handleErrors(error: any): { [key: string]: string } | any {
    if (error && error.error && error.error.errors) {
      const errors: { [key: string]: string } = {};
      
      error.error.errors.forEach((err: any) => {
        errors[err.field] = err.message;
      });
      
      return errors;
    } else if (error && error.error) {
      return error.error;
    } else {
      return '';
    }
  }
}