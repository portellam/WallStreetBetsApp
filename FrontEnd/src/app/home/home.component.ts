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
  _Stock: string = '';
  _MarketStack: MarketStack | undefined;

  // WallStreetBets
  _WallStreetBetsInfoArray: WallStreetBetsInfo[] = [];

  // Favorite
  _Favorite: Favorite = {
    id: 0,
    ticker: '',
    username: ''
  }
  _ticker: string = '';

  // Note
  _description: string = '';
  _Favorite_id: number = 0;

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
  showMarketStack(ticker: string){
    this._MarketStackService.get(ticker,
      (results: any) => {
        console.log('STACK INFO:');
        console.log(results);
        this._MarketStack = results;
      }
    );
  }
  setCurrentStock(stock: string){
    this._Stock = stock;
  }

  // WallStreetBets
  showWsbInfo(){
    this._WallStreetBetsInfoService.get(
      (results: any) => {
        this._WallStreetBetsInfoArray = results;
      }
    );
  }

  // Favorite
  // NOTE: OK, this function is working
  // What I need to do is figure out how to pass the ticker they are favoriting into _Favorite
  // I also need to figure out how to pass their username as well (without having to type it)
  addFav(ticker: string){
    //alert(ticker);
    this._FavoriteService.post(ticker,
      (result: any) => {
        //alert('Succesfully added favorite!')
        if (result) {
          this._ticker = ticker;
          this._Favorite_id = result;
        }
      }
    );
  }

  // Note
  saveText(){
    this._ticker = '';
    this._EditNoteService.post(this._Favorite_id, this._description, (result: any) => this.clear_description());
    // this.EditNoteService.postNote(this._Favorite_id, this._description, (result: any) => {});
  }  

  cancelText(){
    this._ticker = '';
  }

  clear_description(){
    this._description = '';
  }  
}
