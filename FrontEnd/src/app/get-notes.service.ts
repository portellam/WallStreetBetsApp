import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GetNotes } from './get-notes';

@Injectable({
  providedIn: 'root'
})
export class GetNotesService {

  // METHODS //

  // DEPENDENCIES
  constructor(private _HttpClient: HttpClient) { }

  // CRUD FUNCTIONS

  get(cb: any) {
  //retrieveNotesTableInfo(cb: any) {
    this._HttpClient.get<GetNotes[]>('https://localhost:7262/api/WallStreetBets/notes').subscribe(cb)
  }
}
