import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EditNote } from './edit-note';

@Injectable({
  providedIn: 'root'
})
export class EditNoteService {

  constructor(private http: HttpClient) { }

  editNote(_EditNote: EditNote, cb: any) {
    this.http.put<EditNote>(`https://localhost:7262/api/WallStreetBets/notes?noteID=${_EditNote.noteID}&updatedNoteDescription=${_EditNote.updatedNoteDescription}`, _EditNote).subscribe(cb);
  }
}
