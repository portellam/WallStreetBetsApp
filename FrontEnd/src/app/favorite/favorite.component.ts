import { Component, OnInit } from '@angular/core';
import { JoinResults } from '../join-results';
import { JoinResultsService } from '../join-results.service';
import { EditNote } from '../edit-note';
import { EditNoteService } from '../edit-note.service';
import { DeleteNote } from '../delete-note';
import { DeleteNoteService } from '../delete-note.service';

@Component({
  selector: 'app-favorite',
  templateUrl: './favorite.component.html',
  styleUrls: ['./favorite.component.css']
})
export class FavoriteComponent implements OnInit {

  allJoinResultsForUser: JoinResults[] = [];

  _EditNote: EditNote = {
    noteID: 25,
    updatedNoteDescription: 'testing this'
  }

  _DeleteNote: DeleteNote = {
    noteID: 9
  }

  newNote: string = '';

  constructor(private JoinResultsService: JoinResultsService,
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
        alert('Successfully updated the test note');
        this.getUserJoinResults();
      }
    );
  }
  // Example URL: https://localhost:7262/api/WallStreetBets/notes?noteID=25&updatedNoteDescription=this%20looks%20like%20a%20great%20stock

  deleteStockNote() {
    this.DeleteNoteService.deleteNote(this._DeleteNote,
      (result: any) => {
        alert('the GME 1 note should have been deleted')
        this.getUserJoinResults();
      }
      )
  }

}
