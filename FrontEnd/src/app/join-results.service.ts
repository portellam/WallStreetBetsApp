import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { JoinResults } from './join-results';

@Injectable({
  providedIn: 'root'
})
export class JoinResultsService {

  constructor(private http: HttpClient) { }

  
}
