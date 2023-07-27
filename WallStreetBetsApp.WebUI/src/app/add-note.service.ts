import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AddNote } from './add-note';

@Injectable({
  providedIn: 'root'
})
export class AddNoteService {

  constructor(private http: HttpClient) { }

  postNote(_AddNote: AddNote, cb: any){
    this.http.post<AddNote>(`https://localhost:7262/api/WallStreetBets/notes?favID=${_AddNote.favID}&noteDescription=${_AddNote.noteDescription}`, _AddNote).subscribe(cb);
  }
}
