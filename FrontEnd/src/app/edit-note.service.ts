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

  postNote(favid: number, notetext: string, cb: any){
    this.http.post(`https://localhost:7262/api/WallStreetBets/notes?favID=${favid}&noteDescription=${notetext}`, favid).subscribe(cb);
  }

  // https://localhost:7262/api/WallStreetBets/notes?favID=70&noteDescription=hello
}
