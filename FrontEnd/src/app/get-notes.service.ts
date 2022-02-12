import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GetNotes } from './get-notes';

@Injectable({
  providedIn: 'root'
})
export class GetNotesService {

  constructor(private http: HttpClient) { }

  retrieveNotesTableInfo(cb: any){
    this.http.get<GetNotes[]>('https://localhost:7262/api/WallStreetBets/notes').subscribe(cb)
  }
}
