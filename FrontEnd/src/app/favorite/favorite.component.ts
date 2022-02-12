import { Component, OnInit, Input } from '@angular/core';
import { JoinResults } from '../join-results';
import { JoinResultsService } from '../join-results.service';
import { EditNote } from '../edit-note';
import { EditNoteService } from '../edit-note.service';
import { DeleteNote } from '../delete-note';
import { DeleteNoteService } from '../delete-note.service';
import { GetNotes } from '../get-notes';
import { GetNotesService } from '../get-notes.service';

@Component({
  selector: 'app-favorite',
  templateUrl: './favorite.component.html',
  styleUrls: ['./favorite.component.css']
})
export class FavoriteComponent implements OnInit {

  allJoinResultsForUser: JoinResults[] = [];

  _EditNote: EditNote = {
    noteID: 25, 
    updatedNoteDescription: ''
  }

  _DeleteNote: DeleteNote = {
    noteID: 0
  }

  newNote: string = '';

  revealNoteBox: boolean = false;

  /*
  deleteNoteIDcaptured: number = 1;
  getNotesArray: GetNotes[] = [];
  */

  toggleNoteBoxOn(){
    this.revealNoteBox = true;
  }

  toggleNoteBoxOff(){
    this.revealNoteBox = false;
  }


  constructor(private _getNotesService: GetNotesService,
    private JoinResultsService: JoinResultsService,
    private EditNoteService: EditNoteService,
    private DeleteNoteService: DeleteNoteService) { }

  ngOnInit(): void {
    this.getUserJoinResults();
  }

  getUserJoinResults() {
    this.JoinResultsService.getJoinResults(
      (results: any) => {
        this.allJoinResultsForUser = results;
      }
    );
  }
  
  editStockNote() {
    this.EditNoteService.editNote(this._EditNote, 
      (result: any) => {
        alert('hello this is the editStockNote function being called');
        this.getUserJoinResults();
      }
    );
  }
  // Example URL: https://localhost:7262/api/WallStreetBets/notes?noteID=25&updatedNoteDescription=this%20looks%20like%20a%20great%20stock

  // OH MY GOD THIS ACTUALLY MADE IT WORK. I AM GOING TO CRY.
  setNoteID(noteID: number){
    this._DeleteNote.noteID = noteID;
  }

  deleteStockNote() {
    this.DeleteNoteService.deleteNote(this._DeleteNote,
      (result: any) => {
        alert(`Note ID deleted: ${this._DeleteNote.noteID}`)
        this.getUserJoinResults();
      }
      )
  }

  
  /*
  captureNoteID() {
    alert(`Note ID captured... ${this.deleteNoteIDcaptured}`);
  }

  fillNotesArray() {
    this._getNotesService.retrieveNotesTableInfo(
      (results: any) => {
        this.getNotesArray = results;
      }
    )
  }
  */

}
