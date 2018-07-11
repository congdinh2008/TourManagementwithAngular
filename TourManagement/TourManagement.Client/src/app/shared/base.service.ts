import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';





@Injectable({
  providedIn: 'root'
})
export class BaseService {
  apiUrl = environment.apiUrl;

  constructor() { }
}
