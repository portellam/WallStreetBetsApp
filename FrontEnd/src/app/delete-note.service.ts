import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DeleteNote } from './delete-note';

@Injectable({
  providedIn: 'root'
})
export class DeleteNoteService {

  // METHODS //

  // DEPENDENCIES
  constructor(private _HttpClient: HttpClient) { }

  // CRUD FUNCTIONS

  delete(_Note: DeleteNote, cb: any) {
  //deleteNote(_Note: DeleteNote, cb: any) {
    this._HttpClient.delete(`https://localhost:7262/api/WallStreetBets/notes?noteID=${_Note.noteID}`).subscribe(cb);
  }

  // EXAMPLE URL: https://localhost:7262/api/WallStreetBets/notes?noteID=20
}
