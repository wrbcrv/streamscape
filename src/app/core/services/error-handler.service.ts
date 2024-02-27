import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService {

  constructor() { }

  handleErrors(error: any): { [key: string]: string } {
    const errors: { [key: string]: string } = {};

    if (error && error.error && error.error.errors) {
      error.error.errors.forEach((err: any) => {
        errors[err.field] = err.message;
      });
    }

    if (error && error.error) {
      
    }
    
    return errors;
  }
}