import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AddNote } from './add-note';

@Injectable({
  providedIn: 'root'
})
export class AddNoteService {

  // METHODS // 

  // DEPENDENCIES
  constructor(private _HttpClient: HttpClient) { }

  // CRUD FUNCTIONS
  
  //post(_Note: AddNote, cb: any) {
  postNote(_Note: AddNote, cb: any) {
    this._HttpClient.post<AddNote>(`https://localhost:7262/api/WallStreetBets/notes?favID=${_Note.favID}&noteDescription=${_Note.noteDescription}`, _Note).subscribe(cb);
  }
}
