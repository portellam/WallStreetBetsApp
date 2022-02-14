import { Component, OnInit, Input } from '@angular/core';
import { JoinResults } from '../join-results';
import { JoinResultsService } from '../join-results.service';
import { EditNote } from '../edit-note';
import { EditNoteService } from '../edit-note.service';
import { DeleteNote } from '../delete-note';
import { DeleteNoteService } from '../delete-note.service';
import { GetNotes } from '../get-notes';
import { GetNotesService } from '../get-notes.service';
import { AddNote } from '../add-note';
import { AddNoteService } from '../add-note.service';
import { DeleteFavorite } from '../delete-favorite';
import { DeleteFavoriteService } from '../delete-favorite.service';

@Component({
  selector: 'app-favorite',
  templateUrl: './favorite.component.html',
  styleUrls: ['./favorite.component.css']
})
export class FavoriteComponent implements OnInit {

  allJoinResultsForUser: JoinResults[] = [];

  _EditNote: EditNote = {
    noteID: 0, 
    updatedNoteDescription: ''
  }

  _DeleteNote: DeleteNote = {
    noteID: 0
  }
  
  newNote: string = '';
  revealNoteBox: boolean = false;
  passedNoteID: number = 0;
  specificTicker: string = '';
  noteExists: boolean = false;

  toggleNoteExists(noteDesc: string){
    if(noteDesc == null){
      this.noteExists = false;
    }
    else{
      this.noteExists = true;
    }
  }

  setTempTicker(tic: string){
    this.specificTicker = tic;
  }



  /*
  deleteNoteIDcaptured: number = 1;
  getNotesArray: GetNotes[] = [];
  */

  toggleNoteBoxOn(_passedNoteID: number){
    this.revealNoteBox = true;
    this.passedNoteID = _passedNoteID;
  }

  toggleNoteBoxOff(){
    this.revealNoteBox = false;
  }


  constructor(private _deleteFavoriteService: DeleteFavoriteService,
    private _addNoteService: AddNoteService,
    private _getNotesService: GetNotesService,
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

  setEditNoteID(myNoteID: number){
    this._EditNote.noteID = myNoteID;
  }

  
  editStockNote() {
    this.EditNoteService.editNote(this._EditNote, 
      (result: any) => {
        alert(`Edit Note ID: ${this._EditNote.noteID}`);
        this.clearEditText();
        this.toggleNoteBoxOff();
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

  clearEditText(){
    this._EditNote.updatedNoteDescription = '';
  }


  checkIfFavoriteHasNote(_favID: number)
  {
    for (let i: number = 0; i < this.allJoinResultsForUser.length; i++)
    {
      if (this.allJoinResultsForUser[i].favorite_id == _favID)
      {
        if (this.allJoinResultsForUser[i].note_id == null)
        {
          this.favoriteHasNote == false;
          alert(`Statement evalutes to: ${this.favoriteHasNote}`)
        }
      }
    }
  }






  // TO DO: Put the Add Note feature inside an ng-container
  // Make this similar to how the Edit Note works (basically so it only shows the Add Note for just the one stock)

  _AddNote: AddNote = {
    favID: 0,
    noteDescription: ''
  }

  favoriteHasNote: boolean = false;

  setAddNoteFavID(_addNoteFavID: number){
    this._AddNote.favID = _addNoteFavID; 
  }
  
  addNote()
  {
    if (this.favoriteHasNote)
    {
      alert('You already have a note for this favorited stock')
    }
    else
    {
      this._addNoteService.postNote(this._AddNote, 
        (result: any) => {
          alert('Note has been added!')
          this.getUserJoinResults();
        }
      )
    }
  }

  // OMG THIS IS WORKING!
  alternativeAddNote(favID: number)
  {
    let tempDescription: string = '';

    for (let i: number = 0; i < this.allJoinResultsForUser.length; i++)
    {
      if (this.allJoinResultsForUser[i].favorite_id == favID)
      {
        tempDescription = this.allJoinResultsForUser[i].description
        if (tempDescription == null)
        {
          this._addNoteService.postNote(this._AddNote, 
            (result: any) => {
              alert('Note has been added!')
              this.clearAddNoteText();
              this.getUserJoinResults();
            }
          );
        }
        else
        {
          alert('You already have a note for this stock!')
          this.clearAddNoteText();
          this.getUserJoinResults();
        }
      }
    }
  }
  
  clearAddNoteText(){
    this._AddNote.noteDescription = '';
  }






  _DeleteFavorite: DeleteFavorite = {
    username: '',
    ticker: ''
  }

  setDeleteFavoriteInfo(u: string, t: string){
    this._DeleteFavorite.username = u;
    this._DeleteFavorite.ticker = t;
  }

  
  deleteFavorite() {
    this._deleteFavoriteService.deleteFavorite(this._DeleteFavorite,
      (result: any) => {
        alert(`Favorite Deleted: ${this._DeleteFavorite.ticker}`)
        this.getUserJoinResults();
      }
    );
  }



}
