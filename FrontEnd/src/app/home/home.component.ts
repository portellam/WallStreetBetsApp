import { Component, OnInit } from '@angular/core';
import { WallStreetBetsInfo } from '../wall-street-bets-info';
import { WallStreetBetsInfoService } from '../wall-street-bets-info.service';
import { MarketStackService } from '../market-stack.service';
import { MarketStack } from '../market-stack';
import { Favorite } from '../favorite';
import { FavoriteService } from '../favorite.service';
import { EditNoteService } from '../edit-note.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  // PROPERTIES //
  // MarketStack
  currentStock: string = '';
  marketStackInfo: MarketStack | undefined;

  // WallStreetBets
  wsbArray: WallStreetBetsInfo[] = [];

  // Favorite
  myFav: Favorite = {
    id: 0,
    ticker: '',
    username: ''
  }

  // Note
  showFavComment: string = '';
  showFavId: number = 0;
  noteText: string = '';
  
  // ================================================================================ //

  // METHODS //

  // DEPENDENCIES
  constructor(private _EditNoteService: EditNoteService,
    private _WallStreetBetsInfoService: WallStreetBetsInfoService,
    private _MarketStackService: MarketStackService,
    private _FavoriteService: FavoriteService) { }

  ngOnInit(): void {
  }
  
  // MarketStack
  showMarketStackInfo(ticker: string){
    this._MarketStackService.get(ticker,
      (results: any) => {
        console.log('STACK INFO:');
        console.log(results);
        this.marketStackInfo = results;
      }
    );
  }
  setCurrentStock(stock: string){
    this.currentStock = stock;
  }

  // WallStreetBets
  showWsbInfo(){
    this._WallStreetBetsInfoService.get(
      (results: any) => {
        this.wsbArray = results;
      }
    );
  }

  // Favorite
  // NOTE: OK, this function is working
  // What I need to do is figure out how to pass the ticker they are favoriting into myFav
  // I also need to figure out how to pass their username as well (without having to type it)
  addFav(ticker: string){
    //alert(ticker);
    this._FavoriteService.post(ticker,
      (result: any) => {
        //alert('Succesfully added favorite!')
        if (result) {
          this.showFavComment = ticker;
          this.showFavId = result;
        }
      }
    );
  }

  // Note
  saveText(){
    this.showFavComment = '';
    this._EditNoteService.post(this.showFavId, this.noteText, (result: any) => this.clearNoteText());
    // this.EditNoteService.postNote(this.showFavId, this.noteText, (result: any) => {});
  }  

  cancelText(){
    this.showFavComment = '';
  }

  clearNoteText(){
    this.noteText = '';
  }  
}
