import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EditNote } from './edit-note';

@Injectable({
  providedIn: 'root'
})
export class EditNoteService {

  // METHODS //

  // DEPENDENCIES
  constructor(private _HttpClient: HttpClient) { }

  // CRUD FUNCTIONS

  post(fav_id: number, description: string, cb: any) {
  //postNote(fav_id: number, description: string, cb: any) {
    this._HttpClient.post(`https://localhost:7262/api/WallStreetBets/notes?favID=${fav_id}&noteDescription=${description}`, fav_id).subscribe(cb);
  }

  put(_Note: EditNote, cb: any) {
  //editNote(_Note: EditNote, cb: any) {
    this._HttpClient.put<EditNote>(`https://localhost:7262/api/WallStreetBets/notes?noteID=${_Note.noteID}&updatedNoteDescription=${_Note.updatedNoteDescription}`, _Note).subscribe(cb);
  }

  // EXAMPLE URL: https://localhost:7262/api/WallStreetBets/notes?favID=70&noteDescription=hello
}
