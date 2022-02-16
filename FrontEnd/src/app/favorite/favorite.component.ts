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
import { WallStreetBetsInfo } from '../wall-street-bets-info';
import { WallStreetBetsInfoService } from '../wall-street-bets-info.service';
import { MarketStack } from '../market-stack';
import { MarketStackService } from '../market-stack.service';

@Component({
  selector: 'app-favorite',
  templateUrl: './favorite.component.html',
  styleUrls: ['./favorite.component.css']
})
export class FavoriteComponent implements OnInit {

  // PROPERTIES //
  // MarketStack
  _MarketStackObject: MarketStack | undefined

  // WallStreetBets
  fav_WSBArray: WallStreetBetsInfo[] = [];
  temp_WSBInfoObj: WallStreetBetsInfo | undefined;

  // JoinResults
  _JoinResults: JoinResults[] = [];

  // Favorite
  _DeleteFavorite: DeleteFavorite = {
    username: '',
    ticker: ''
  }
  _ticker: string = '';
  specific_Ticker: string = '';

  // Notes
  _AddNote: AddNote = {
    favID: 0,
    noteDescription: ''
  }
  _EditNote: EditNote = {
    noteID: 0, 
    updatedNoteDescription: ''
  }
  _DeleteNote: DeleteNote = {
    noteID: 0
  }
  _Note: string = '';
  _note_ID: number = 0;

  // TOGGLES
  favoriteNoteVisible: boolean = false;
  noteExists: boolean = false;
  noteBoxVisible: boolean = false;
  temp_WSBInfoObjFilled: boolean = false;

  // NOTE: commented out by Josh
  /*
  deleteNoteIDcaptured: number = 1;
  getNotesArray: GetNotes[] = [];
  */

  // ================================================================================ //

  // METHODS //

  // DEPENDENCIES
  constructor(private _MarketStackService: MarketStackService,
    private _WallStreetBetsInfoService: WallStreetBetsInfoService,
    private _DeleteFavoriteService: DeleteFavoriteService,
    private _AddNoteService: AddNoteService,
    private _GetNotesService: GetNotesService,
    private _JoinResultsService: JoinResultsService,
    private _EditNoteService: EditNoteService,
    private _DeleteNoteService: DeleteNoteService) { }

  ngOnInit(): void {
    this.getUserJoinResults();
    this.captureFavoriteWsbInfo();
  }

  // JoinResults
  getUserJoinResults() {
    this._JoinResultsService.get(
      (results: any) => {
        this._JoinResults = results;
      }
    );
  }
  // Example URL: https://localhost:7262/api/WallStreetBets/notes?noteID=25&updatedNoteDescription=this%20looks%20like%20a%20great%20stock
  // OH MY GOD THIS ACTUALLY MADE IT WORK. I AM GOING TO CRY.

  // MarketStack
  showMarketStackInfoForStock(ticker: string){
    this._MarketStackService.get(ticker,
      (results: any) => {
        this._MarketStackObject = results;  
      }
    );
  }

  // WallStreetBets
  captureFavoriteWsbInfo() {
    this._WallStreetBetsInfoService.get(
      (results: any) => {
        this.fav_WSBArray = results;
      }
    );
  }

  showWsbInfoForStock(myFavoriteTicker: string){
    for (let i: number = 0; i < this.fav_WSBArray.length; i++){
      if (this.fav_WSBArray[i].ticker == myFavoriteTicker){
        this.temp_WSBInfoObj = this.fav_WSBArray[i];
        this.temp_WSBInfoObjFilled = true;
        this._ticker = this.fav_WSBArray[i].ticker;
      }
    }
  }  
  
  // Favorite
  setDeleteFavoriteInfo(u: string, t: string){
    this._DeleteFavorite.username = u;
    this._DeleteFavorite.ticker = t;
  }

  deleteFavorite() {
    this._DeleteFavoriteService.delete(this._DeleteFavorite,
      (result: any) => {
        alert(`Favorite Deleted: ${this._DeleteFavorite.ticker}`)
        this.getUserJoinResults();
      }
    );
  }

  // Note
  // TODO: Put the Add Note feature inside an ng-container
  // Make this similar to how the Edit Note works (basically so it only shows the Add Note for just the one stock)

  toggleNoteExists(noteDesc: string){
    if(noteDesc == null){
      this.noteExists = false;
    }
    else{
      this.noteExists = true;
    }
  }

  toggleNoteBoxOn(__note_ID: number){
    this.noteBoxVisible = true;
    this._note_ID = __note_ID;
  }

  toggleNoteBoxOff(){
    this.noteBoxVisible = false;
  }

  setTempTicker(tic: string){
    this.specific_Ticker = tic;
  }

  setEditNoteID(myNoteID: number){
    this._EditNote.noteID = myNoteID;
  }

  editStockNote() {
    this._EditNoteService.put(this._EditNote, 
      (result: any) => {
        alert(`Edit Note ID: ${this._EditNote.noteID}`);
        this.clearEditText();
        this.toggleNoteBoxOff();
        this.getUserJoinResults();
      }
    );
  }

  deleteStockNote() {
    this._DeleteNoteService.delete(this._DeleteNote,
      (result: any) => {
        alert(`Note ID deleted: ${this._DeleteNote.noteID}`)
        this.getUserJoinResults();
      }
      )
  }

  setNoteID(noteID: number){
    this._DeleteNote.noteID = noteID;
  }

  checkIfFavoriteNoteVisible(_favID: number)
  {
    for (let i: number = 0; i < this._JoinResults.length; i++)
    {
      if (this._JoinResults[i].favorite_id == _favID)
      {
        if (this._JoinResults[i].note_id == null)
        {
          this.favoriteNoteVisible == false;
          alert(`Statement evalutes to: ${this.favoriteNoteVisible}`)
        }
      }
    }
  }

  setAddNoteFavID(_addNoteFavID: number){
    this._AddNote.favID = _addNoteFavID; 
  }

  addNote()
  {
    if (this.favoriteNoteVisible)
    {
      alert('You already have a note for this favorited stock')
    }
    else
    {
      this._AddNoteService.postNote(this._AddNote, 
        (result: any) => {
          alert('Note has been added!')
          this.getUserJoinResults();
        }
      )
    }
  }

  // NOTE: OMG THIS IS WORKING!
  alternativeAddNote(favID: number)
  {
    let tempDescription: string = '';

    for (let i: number = 0; i < this._JoinResults.length; i++)
    {
      if (this._JoinResults[i].favorite_id == favID)
      {
        tempDescription = this._JoinResults[i].description
        if (tempDescription == null)
        {
          this._AddNoteService.postNote(this._AddNote, 
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

  clearEditText(){
    this._EditNote.updatedNoteDescription = '';
  }
}
